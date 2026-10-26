using System.Data;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace PlataformadeJogos
{
    public partial class Form1 : Form
    {
        private readonly HttpClient http = new()
        {
            // Endereço da API (perfil http do PlataformadeJogos). Ajuste se a porta for outra.
            BaseAddress = new Uri("http://localhost:5041/")
        };

        private readonly Dictionary<string, TextBox> fieldInputs = new();
        private EntityDefinition currentEntity = EntityDefinitions.Games;
        private ScreenMode currentMode;
        private TextBox? idInput;
        private Button? saveButton;

        public Form1()
        {
            InitializeComponent();
            ConfigureWorkspace();
            ConnectMenuActions();
        }

        private void ConfigureWorkspace()
        {
            panel1.Location = new Point(20, menuStrip1.Bottom + 12);
            panel1.Size = new Size(ClientSize.Width - 40, ClientSize.Height - menuStrip1.Bottom - 32);
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Padding = new Padding(18);
            panel1.BackColor = Color.WhiteSmoke;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Visible = false;
        }

        private void ConnectMenuActions()
        {
            editarCadastroJogoToolStripMenuItem.Click += (_, _) => ShowEditor(EntityDefinitions.Games, ScreenMode.Edit);
            editarCategoriaDeJogoToolStripMenuItem.Click += (_, _) => ShowEditor(EntityDefinitions.Categories, ScreenMode.Edit);
            jogosToolStripMenuItem1.Click += async (_, _) => await ShowListAsync(EntityDefinitions.Games);
            categoriasToolStripMenuItem.Click += async (_, _) => await ShowListAsync(EntityDefinitions.Categories);
            excluirJogosDaBibliotecaToolStripMenuItem.Click += (_, _) => ShowEditor(EntityDefinitions.Games, ScreenMode.Delete);
            excluirCategoriaToolStripMenuItem.Click += (_, _) => ShowEditor(EntityDefinitions.Categories, ScreenMode.Delete);

            cadastrarUsuáriosToolStripMenuItem.Click += (_, _) => ShowEditor(EntityDefinitions.Users, ScreenMode.Create);
            editarUsuáriosToolStripMenuItem.Click += (_, _) => ShowEditor(EntityDefinitions.Users, ScreenMode.Edit);
            explorarUsuáriosToolStripMenuItem.Click += async (_, _) => await ShowListAsync(EntityDefinitions.Users);
            excluirUsuáriosToolStripMenuItem.Click += (_, _) => ShowEditor(EntityDefinitions.Users, ScreenMode.Delete);

            cadastrarEstúdiosToolStripMenuItem.Click += (_, _) => ShowEditor(EntityDefinitions.Studios, ScreenMode.Create);
            editarCadastroDeEstúdiosToolStripMenuItem.Click += (_, _) => ShowEditor(EntityDefinitions.Studios, ScreenMode.Edit);
            explorarEstúdiosToolStripMenuItem.Click += async (_, _) => await ShowListAsync(EntityDefinitions.Studios);
            excluirEstúdiosToolStripMenuItem.Click += (_, _) => ShowEditor(EntityDefinitions.Studios, ScreenMode.Delete);
        }

        private void novoJogoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowEditor(EntityDefinitions.Games, ScreenMode.Create);
        }

        private void novaCategoriaDeJogoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowEditor(EntityDefinitions.Categories, ScreenMode.Create);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ = SaveEntityAsync();
        }

        private void ShowEditor(EntityDefinition entity, ScreenMode mode)
        {
            currentEntity = entity;
            currentMode = mode;
            fieldInputs.Clear();
            idInput = null;
            panel1.Controls.Clear();
            panel1.Visible = true;

            var page = CreatePageLayout(mode == ScreenMode.Create ? $"Cadastrar {entity.Singular}" :
                mode == ScreenMode.Edit ? $"Editar {entity.Singular}" : $"Excluir {entity.Singular}");
            var body = CreateScrollableBody();
            page.Controls.Add(body, 0, 1);

            var content = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Dock = DockStyle.Top,
                Padding = new Padding(12)
            };
            body.Controls.Add(content);

            if (mode == ScreenMode.Edit || mode == ScreenMode.Delete)
            {
                AddIdInput(content, mode == ScreenMode.Edit);
            }

            if (mode != ScreenMode.Delete)
            {
                foreach (var field in entity.Fields.Where(field => mode != ScreenMode.Edit || !field.CreateOnly))
                {
                    AddFieldInput(content, field);
                }
            }

            var actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0, 8, 0, 0)
            };
            page.Controls.Add(actions, 0, 2);

            if (mode == ScreenMode.Edit)
            {
                var loadButton = CreateButton("Buscar registro");
                loadButton.Click += async (_, _) => await LoadEntityAsync();
                actions.Controls.Add(loadButton);
            }

            saveButton = CreateButton(mode == ScreenMode.Create ? "Cadastrar" : mode == ScreenMode.Edit ? "Salvar alterações" : "Excluir");
            saveButton.Click += button1_Click;
            actions.Controls.Add(saveButton);

            var cancelButton = CreateButton("Fechar");
            cancelButton.Click += (_, _) => panel1.Visible = false;
            actions.Controls.Add(cancelButton);
        }

        private async Task ShowListAsync(EntityDefinition entity)
        {
            currentEntity = entity;
            currentMode = ScreenMode.List;
            panel1.Controls.Clear();
            panel1.Visible = true;

            var page = CreatePageLayout($"Explorar {entity.Plural}");
            var body = CreateScrollableBody();
            page.Controls.Add(body, 0, 1);
            var refreshButton = CreateButton("Atualizar lista");
            refreshButton.Click += async (_, _) => await LoadListAsync(entity, body);
            page.Controls.Add(refreshButton, 0, 2);

            await LoadListAsync(entity, body);
        }

        private async Task LoadListAsync(EntityDefinition entity, Panel body)
        {
            try
            {
                // GET api/{recurso}: busca todos os registros para a tela Explorar.
                using var response = await http.GetAsync(entity.Endpoint);
                await EnsureSuccessAsync(response);
                using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                var records = GetArrayFromResponse(document.RootElement);
                var table = CreateTable(records, entity);
                body.Controls.Clear();
                body.Controls.Add(new DataGridView
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    DataSource = table,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    MultiSelect = false,
                    BackgroundColor = Color.White
                });
            }
            catch (Exception exception)
            {
                ShowRequestError(exception);
            }
        }

        private async Task LoadEntityAsync()
        {
            if (!TryGetId(out var id))
            {
                return;
            }

            try
            {
                // GET api/{recurso}/{id}: carrega o registro escolhido para edição.
                using var response = await http.GetAsync($"{currentEntity.Endpoint}/{Uri.EscapeDataString(id)}");
                await EnsureSuccessAsync(response);
                using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                var record = document.RootElement;
                if (record.ValueKind == JsonValueKind.Object && record.TryGetProperty("data", out var data))
                {
                    record = data;
                }

                foreach (var field in currentEntity.Fields.Where(field => fieldInputs.ContainsKey(field.Name)))
                {
                    if (record.TryGetProperty(field.Name, out var value))
                    {
                        fieldInputs[field.Name].Text = value.ValueKind == JsonValueKind.Null ? string.Empty : value.ToString();
                    }
                }

                MessageBox.Show("Registro carregado. Faça as alterações e clique em Salvar alterações.", "Registro encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                ShowRequestError(exception);
            }
        }

        private async Task SaveEntityAsync()
        {
            if (currentMode == ScreenMode.Delete)
            {
                await DeleteEntityAsync();
                return;
            }

            if (!TryBuildPayload(out var payload))
            {
                return;
            }

            try
            {
                HttpResponseMessage response;
                if (currentMode == ScreenMode.Create)
                {
                    // POST api/{recurso}: envia os dados validados para cadastrar um registro.
                    response = await http.PostAsJsonAsync(currentEntity.Endpoint, payload);
                }
                else
                {
                    if (!TryGetId(out var id))
                    {
                        return;
                    }

                    // PUT api/{recurso}/{id}: envia os dados atualizados do registro.
                    response = await http.PutAsJsonAsync($"{currentEntity.Endpoint}/{Uri.EscapeDataString(id)}", payload);
                }

                using (response)
                {
                    await EnsureSuccessAsync(response);
                }

                MessageBox.Show(currentMode == ScreenMode.Create ? "Registro cadastrado com sucesso." : "Registro atualizado com sucesso.", "Concluído", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panel1.Visible = false;
            }
            catch (Exception exception)
            {
                ShowRequestError(exception);
            }
        }

        private async Task DeleteEntityAsync()
        {
            if (!TryGetId(out var id))
            {
                return;
            }

            var confirmation = MessageBox.Show($"Deseja realmente excluir o registro de ID {id}?", "Confirmar exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            try
            {
                // DELETE api/{recurso}/{id}: remove o registro após confirmação do usuário.
                using var response = await http.DeleteAsync($"{currentEntity.Endpoint}/{Uri.EscapeDataString(id)}");
                await EnsureSuccessAsync(response);
                MessageBox.Show("Registro excluído com sucesso.", "Concluído", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panel1.Visible = false;
            }
            catch (Exception exception)
            {
                ShowRequestError(exception);
            }
        }

        private bool TryBuildPayload(out Dictionary<string, object?> payload)
        {
            payload = new Dictionary<string, object?>();
            foreach (var field in currentEntity.Fields.Where(field => fieldInputs.ContainsKey(field.Name)))
            {
                var text = fieldInputs[field.Name].Text.Trim();
                if (field.Required && string.IsNullOrWhiteSpace(text))
                {
                    return ShowValidationError($"O campo {field.Label} é obrigatório.", fieldInputs[field.Name]);
                }

                if (text.Length > field.MaxLength)
                {
                    return ShowValidationError($"O campo {field.Label} deve ter no máximo {field.MaxLength} caracteres.", fieldInputs[field.Name]);
                }

                if (string.IsNullOrWhiteSpace(text))
                {
                    continue;
                }

                switch (field.Type)
                {
                    case FieldType.Email:
                        try
                        {
                            var email = new System.Net.Mail.MailAddress(text);
                            if (!string.Equals(email.Address, text, StringComparison.OrdinalIgnoreCase))
                            {
                                return ShowValidationError("Informe um e-mail válido.", fieldInputs[field.Name]);
                            }
                        }
                        catch (FormatException)
                        {
                            return ShowValidationError("Informe um e-mail válido.", fieldInputs[field.Name]);
                        }
                        payload[field.Name] = text;
                        break;
                    case FieldType.Decimal:
                        if (!decimal.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, out var amount) || amount < 0)
                        {
                            return ShowValidationError($"O campo {field.Label} deve ser um número igual ou maior que zero.", fieldInputs[field.Name]);
                        }
                        payload[field.Name] = amount;
                        break;
                    case FieldType.Integer:
                        if (!int.TryParse(text, NumberStyles.Integer, CultureInfo.CurrentCulture, out var number) || number <= 0)
                        {
                            return ShowValidationError($"O campo {field.Label} deve ser um número inteiro maior que zero.", fieldInputs[field.Name]);
                        }
                        payload[field.Name] = number;
                        break;
                    default:
                        payload[field.Name] = text;
                        break;
                }
            }

            return true;
        }

        private bool ShowValidationError(string message, Control control)
        {
            MessageBox.Show(message, "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            control.Focus();
            return false;
        }

        private bool TryGetId(out string id)
        {
            id = idInput?.Text.Trim() ?? string.Empty;
            if (!int.TryParse(id, NumberStyles.Integer, CultureInfo.InvariantCulture, out var numericId) || numericId <= 0)
            {
                MessageBox.Show("Informe um ID numérico maior que zero.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                idInput?.Focus();
                return false;
            }

            id = numericId.ToString(CultureInfo.InvariantCulture);
            return true;
        }

        private void AddIdInput(FlowLayoutPanel content, bool includeLoadHint)
        {
            var row = new FlowLayoutPanel { AutoSize = true, WrapContents = false, Margin = new Padding(3, 8, 3, 8) };
            row.Controls.Add(new Label { Text = "ID do registro:", AutoSize = true, Width = 150, Padding = new Padding(0, 7, 0, 0) });
            idInput = new TextBox { Width = 280, MaxLength = 10 };
            row.Controls.Add(idInput);
            if (includeLoadHint)
            {
                row.Controls.Add(new Label { Text = "Informe o ID e clique em Buscar registro.", AutoSize = true, Padding = new Padding(8, 7, 0, 0) });
            }
            content.Controls.Add(row);
        }

        private void AddFieldInput(FlowLayoutPanel content, FieldDefinition field)
        {
            var row = new FlowLayoutPanel { AutoSize = true, WrapContents = false, Margin = new Padding(3, 6, 3, 6) };
            row.Controls.Add(new Label { Text = field.Label + (field.Required ? " *:" : ":"), AutoSize = true, Width = 150, Padding = new Padding(0, 7, 0, 0) });
            var input = new TextBox
            {
                Width = 360,
                MaxLength = field.MaxLength,
                UseSystemPasswordChar = field.Type == FieldType.Password,
                Multiline = field.Multiline,
                Height = field.Multiline ? 72 : 30,
                ScrollBars = field.Multiline ? ScrollBars.Vertical : ScrollBars.None
            };
            input.Name = field.Name;
            fieldInputs.Add(field.Name, input);
            row.Controls.Add(input);
            content.Controls.Add(row);
        }

        private TableLayoutPanel CreatePageLayout(string title)
        {
            var page = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Color.WhiteSmoke
            };
            page.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            page.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
            page.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            page.RowStyles.Add(new RowStyle(SizeType.Absolute, 54));
            page.Controls.Add(new Label
            {
                Text = title,
                Dock = DockStyle.Fill,
                Font = new Font("Showcard Gothic", 16F, FontStyle.Regular, GraphicsUnit.Point),
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.FromArgb(45, 45, 65)
            }, 0, 0);
            panel1.Controls.Add(page);
            return page;
        }

        private static Panel CreateScrollableBody() => new()
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
            Padding = new Padding(6),
            BackColor = Color.White
        };

        private static Button CreateButton(string text) => new()
        {
            Text = text,
            AutoSize = true,
            MinimumSize = new Size(130, 38),
            Margin = new Padding(5),
            Font = new Font("Segoe UI", 9.5F, FontStyle.Regular)
        };

        private static JsonElement GetArrayFromResponse(JsonElement root)
        {
            if (root.ValueKind == JsonValueKind.Array)
            {
                return root;
            }

            if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("data", out var data) && data.ValueKind == JsonValueKind.Array)
            {
                return data;
            }

            throw new JsonException("A API deve retornar uma lista JSON ou um objeto com a propriedade 'data' contendo a lista.");
        }

        private static DataTable CreateTable(JsonElement records, EntityDefinition entity)
        {
            var table = new DataTable();
            var visibleFields = entity.Fields.Where(field => field.Type != FieldType.Password).Select(field => field.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
            var columns = new List<string>();
            foreach (var record in records.EnumerateArray())
            {
                if (record.ValueKind != JsonValueKind.Object)
                {
                    continue;
                }

                foreach (var property in record.EnumerateObject())
                {
                    if (property.Name.Equals("senha", StringComparison.OrdinalIgnoreCase) || property.Name.Equals("password", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    if (!columns.Contains(property.Name, StringComparer.OrdinalIgnoreCase))
                    {
                        columns.Add(property.Name);
                        var columnName = property.Name;
                        table.Columns.Add(columnName, typeof(string));
                        if (visibleFields.Contains(columnName))
                        {
                            table.Columns[columnName]!.Caption = entity.Fields.First(field => field.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase)).Label;
                        }
                    }
                }
            }

            foreach (var record in records.EnumerateArray())
            {
                if (record.ValueKind != JsonValueKind.Object)
                {
                    continue;
                }

                var row = table.NewRow();
                foreach (var column in columns)
                {
                    if (record.TryGetProperty(column, out var value))
                    {
                        row[column] = value.ValueKind == JsonValueKind.Null ? string.Empty : value.ToString();
                    }
                }
                table.Rows.Add(row);
            }

            return table;
        }

        private static async Task EnsureSuccessAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            var details = await response.Content.ReadAsStringAsync();
            if (details.Length > 500)
            {
                details = details[..500];
            }

            var message = $"A API retornou {(int)response.StatusCode} ({response.ReasonPhrase}).";
            if (!string.IsNullOrWhiteSpace(details))
            {
                message += $"\n\nDetalhes: {details}";
            }
            throw new HttpRequestException(message, null, response.StatusCode);
        }

        private static void ShowRequestError(Exception exception)
        {
            var message = exception switch
            {
                HttpRequestException when exception is HttpRequestException request && request.StatusCode == HttpStatusCode.NotFound => "Registro não encontrado. Verifique o ID informado.\n\n" + exception.Message,
                HttpRequestException => "Não foi possível concluir a requisição HTTP. Verifique se a API está em execução e confira o endereço/porta configurados.\n\n" + exception.Message,
                TaskCanceledException => "A requisição excedeu o tempo limite.",
                JsonException => "A resposta da API não está no formato JSON esperado.\n\n" + exception.Message,
                _ => "Ocorreu um erro ao comunicar com a API.\n\n" + exception.Message
            };
            MessageBox.Show(message, "Erro na requisição", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private enum ScreenMode { Create, Edit, Delete, List }
        private enum FieldType { Text, Decimal, Integer, Email, Password }

        private sealed record FieldDefinition(string Name, string Label, FieldType Type = FieldType.Text, bool Required = true, int MaxLength = 120, bool Multiline = false, bool CreateOnly = false);
        private sealed record EntityDefinition(string Endpoint, string Singular, string Plural, FieldDefinition[] Fields);

        private static class EntityDefinitions
        {
            public static readonly EntityDefinition Games = new("api/jogos", "jogo", "jogos", new[]
            {
                new FieldDefinition("nome", "Nome do jogo", MaxLength: 100),
                new FieldDefinition("descricao", "Descrição", Required: false, MaxLength: 500, Multiline: true),
                new FieldDefinition("preco", "Preço", FieldType.Decimal),
                new FieldDefinition("categoriaId", "ID da categoria", FieldType.Integer),
                new FieldDefinition("estudioId", "ID do estúdio", FieldType.Integer)
            });

            public static readonly EntityDefinition Categories = new("api/categorias", "categoria", "categorias", new[]
            {
                new FieldDefinition("nome", "Nome da categoria", MaxLength: 80),
                new FieldDefinition("descricao", "Descrição", Required: false, MaxLength: 300, Multiline: true)
            });

            public static readonly EntityDefinition Studios = new("api/estudios", "estúdio", "estúdios", new[]
            {
                new FieldDefinition("nome", "Nome do estúdio", MaxLength: 120),
                new FieldDefinition("pais", "País", Required: false, MaxLength: 80)
            });

            public static readonly EntityDefinition Users = new("api/usuarios", "usuário", "usuários", new[]
            {
                new FieldDefinition("nome", "Nome", MaxLength: 120),
                new FieldDefinition("email", "E-mail", FieldType.Email, MaxLength: 254),
                new FieldDefinition("senha", "Senha", FieldType.Password, MaxLength: 100, CreateOnly: true)
            });
        }
    }
}

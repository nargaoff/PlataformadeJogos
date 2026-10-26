namespace PlataformadeJogos
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            jogosToolStripMenuItem = new ToolStripMenuItem();
            cadastrarNovoJogoToolStripMenuItem = new ToolStripMenuItem();
            novoJogoToolStripMenuItem = new ToolStripMenuItem();
            novaCategoriaDeJogoToolStripMenuItem = new ToolStripMenuItem();
            editrarCadastroDeJogoToolStripMenuItem = new ToolStripMenuItem();
            editarCadastroJogoToolStripMenuItem = new ToolStripMenuItem();
            editarCategoriaDeJogoToolStripMenuItem = new ToolStripMenuItem();
            explorarJogosToolStripMenuItem = new ToolStripMenuItem();
            jogosToolStripMenuItem1 = new ToolStripMenuItem();
            categoriasToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            excluirJogosDaBibliotecaToolStripMenuItem = new ToolStripMenuItem();
            excluirCategoriaToolStripMenuItem = new ToolStripMenuItem();
            usuáriosToolStripMenuItem = new ToolStripMenuItem();
            cadastrarUsuáriosToolStripMenuItem = new ToolStripMenuItem();
            editarUsuáriosToolStripMenuItem = new ToolStripMenuItem();
            explorarUsuáriosToolStripMenuItem = new ToolStripMenuItem();
            excluirUsuáriosToolStripMenuItem = new ToolStripMenuItem();
            estúdiosToolStripMenuItem = new ToolStripMenuItem();
            cadastrarEstúdiosToolStripMenuItem = new ToolStripMenuItem();
            editarCadastroDeEstúdiosToolStripMenuItem = new ToolStripMenuItem();
            excluirEstúdiosToolStripMenuItem = new ToolStripMenuItem();
            explorarEstúdiosToolStripMenuItem = new ToolStripMenuItem();
            comunidadeToolStripMenuItem = new ToolStripMenuItem();
            discussõesToolStripMenuItem = new ToolStripMenuItem();
            mercadoToolStripMenuItem = new ToolStripMenuItem();
            sobreToolStripMenuItem = new ToolStripMenuItem();
            panel2 = new Panel();
            textBox1 = new TextBox();
            label1 = new Label();
            textBox2 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            comboBox1 = new ComboBox();
            label4 = new Label();
            comboBox2 = new ComboBox();
            button1 = new Button();
            label5 = new Label();
            panel1 = new Panel();
            menuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.GrayText;
            menuStrip1.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { jogosToolStripMenuItem, usuáriosToolStripMenuItem, estúdiosToolStripMenuItem, comunidadeToolStripMenuItem, sobreToolStripMenuItem });
            menuStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(982, 34);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // jogosToolStripMenuItem
            // 
            jogosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cadastrarNovoJogoToolStripMenuItem, editrarCadastroDeJogoToolStripMenuItem, explorarJogosToolStripMenuItem, toolStripSeparator1, excluirJogosDaBibliotecaToolStripMenuItem, excluirCategoriaToolStripMenuItem });
            jogosToolStripMenuItem.Name = "jogosToolStripMenuItem";
            jogosToolStripMenuItem.Size = new Size(89, 30);
            jogosToolStripMenuItem.Text = "Jogos";
            // 
            // cadastrarNovoJogoToolStripMenuItem
            // 
            cadastrarNovoJogoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { novoJogoToolStripMenuItem, novaCategoriaDeJogoToolStripMenuItem });
            cadastrarNovoJogoToolStripMenuItem.Name = "cadastrarNovoJogoToolStripMenuItem";
            cadastrarNovoJogoToolStripMenuItem.Size = new Size(398, 30);
            cadastrarNovoJogoToolStripMenuItem.Text = "Cadastrar";
            // 
            // novoJogoToolStripMenuItem
            // 
            novoJogoToolStripMenuItem.Name = "novoJogoToolStripMenuItem";
            novoJogoToolStripMenuItem.Size = new Size(352, 30);
            novoJogoToolStripMenuItem.Text = "Novo jogo";
            novoJogoToolStripMenuItem.Click += novoJogoToolStripMenuItem_Click;
            // 
            // novaCategoriaDeJogoToolStripMenuItem
            // 
            novaCategoriaDeJogoToolStripMenuItem.Name = "novaCategoriaDeJogoToolStripMenuItem";
            novaCategoriaDeJogoToolStripMenuItem.Size = new Size(352, 30);
            novaCategoriaDeJogoToolStripMenuItem.Text = "Nova categoria de jogo";
            novaCategoriaDeJogoToolStripMenuItem.Click += novaCategoriaDeJogoToolStripMenuItem_Click;
            // 
            // editrarCadastroDeJogoToolStripMenuItem
            // 
            editrarCadastroDeJogoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { editarCadastroJogoToolStripMenuItem, editarCategoriaDeJogoToolStripMenuItem });
            editrarCadastroDeJogoToolStripMenuItem.Name = "editrarCadastroDeJogoToolStripMenuItem";
            editrarCadastroDeJogoToolStripMenuItem.Size = new Size(398, 30);
            editrarCadastroDeJogoToolStripMenuItem.Text = "Editrar";
            // 
            // editarCadastroJogoToolStripMenuItem
            // 
            editarCadastroJogoToolStripMenuItem.Name = "editarCadastroJogoToolStripMenuItem";
            editarCadastroJogoToolStripMenuItem.Size = new Size(371, 30);
            editarCadastroJogoToolStripMenuItem.Text = "Editar cadastro jogo";
            // 
            // editarCategoriaDeJogoToolStripMenuItem
            // 
            editarCategoriaDeJogoToolStripMenuItem.Name = "editarCategoriaDeJogoToolStripMenuItem";
            editarCategoriaDeJogoToolStripMenuItem.Size = new Size(371, 30);
            editarCategoriaDeJogoToolStripMenuItem.Text = "Editar categoria de jogo";
            // 
            // explorarJogosToolStripMenuItem
            // 
            explorarJogosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { jogosToolStripMenuItem1, categoriasToolStripMenuItem });
            explorarJogosToolStripMenuItem.Name = "explorarJogosToolStripMenuItem";
            explorarJogosToolStripMenuItem.Size = new Size(398, 30);
            explorarJogosToolStripMenuItem.Text = "Explorar";
            // 
            // jogosToolStripMenuItem1
            // 
            jogosToolStripMenuItem1.Name = "jogosToolStripMenuItem1";
            jogosToolStripMenuItem1.Size = new Size(218, 30);
            jogosToolStripMenuItem1.Text = "Jogos";
            // 
            // categoriasToolStripMenuItem
            // 
            categoriasToolStripMenuItem.Name = "categoriasToolStripMenuItem";
            categoriasToolStripMenuItem.Size = new Size(218, 30);
            categoriasToolStripMenuItem.Text = "Categorias";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(395, 6);
            // 
            // excluirJogosDaBibliotecaToolStripMenuItem
            // 
            excluirJogosDaBibliotecaToolStripMenuItem.Name = "excluirJogosDaBibliotecaToolStripMenuItem";
            excluirJogosDaBibliotecaToolStripMenuItem.Size = new Size(398, 30);
            excluirJogosDaBibliotecaToolStripMenuItem.Text = "Excluir jogos da biblioteca";
            // 
            // excluirCategoriaToolStripMenuItem
            // 
            excluirCategoriaToolStripMenuItem.Name = "excluirCategoriaToolStripMenuItem";
            excluirCategoriaToolStripMenuItem.Size = new Size(398, 30);
            excluirCategoriaToolStripMenuItem.Text = "Excluir categoria";
            // 
            // usuáriosToolStripMenuItem
            // 
            usuáriosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cadastrarUsuáriosToolStripMenuItem, editarUsuáriosToolStripMenuItem, explorarUsuáriosToolStripMenuItem, excluirUsuáriosToolStripMenuItem });
            usuáriosToolStripMenuItem.Name = "usuáriosToolStripMenuItem";
            usuáriosToolStripMenuItem.Size = new Size(122, 30);
            usuáriosToolStripMenuItem.Text = "Usuários";
            // 
            // cadastrarUsuáriosToolStripMenuItem
            // 
            cadastrarUsuáriosToolStripMenuItem.Name = "cadastrarUsuáriosToolStripMenuItem";
            cadastrarUsuáriosToolStripMenuItem.Size = new Size(312, 30);
            cadastrarUsuáriosToolStripMenuItem.Text = "Cadastrar usuários";
            // 
            // editarUsuáriosToolStripMenuItem
            // 
            editarUsuáriosToolStripMenuItem.Name = "editarUsuáriosToolStripMenuItem";
            editarUsuáriosToolStripMenuItem.Size = new Size(312, 30);
            editarUsuáriosToolStripMenuItem.Text = "Editar usuários";
            // 
            // explorarUsuáriosToolStripMenuItem
            // 
            explorarUsuáriosToolStripMenuItem.Name = "explorarUsuáriosToolStripMenuItem";
            explorarUsuáriosToolStripMenuItem.Size = new Size(312, 30);
            explorarUsuáriosToolStripMenuItem.Text = "Explorar usuários";
            // 
            // excluirUsuáriosToolStripMenuItem
            // 
            excluirUsuáriosToolStripMenuItem.Name = "excluirUsuáriosToolStripMenuItem";
            excluirUsuáriosToolStripMenuItem.Size = new Size(312, 30);
            excluirUsuáriosToolStripMenuItem.Text = "Excluir usuários";
            // 
            // estúdiosToolStripMenuItem
            // 
            estúdiosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cadastrarEstúdiosToolStripMenuItem, editarCadastroDeEstúdiosToolStripMenuItem, excluirEstúdiosToolStripMenuItem, explorarEstúdiosToolStripMenuItem });
            estúdiosToolStripMenuItem.Name = "estúdiosToolStripMenuItem";
            estúdiosToolStripMenuItem.Size = new Size(121, 30);
            estúdiosToolStripMenuItem.Text = "Estúdios";
            // 
            // cadastrarEstúdiosToolStripMenuItem
            // 
            cadastrarEstúdiosToolStripMenuItem.Name = "cadastrarEstúdiosToolStripMenuItem";
            cadastrarEstúdiosToolStripMenuItem.Size = new Size(406, 30);
            cadastrarEstúdiosToolStripMenuItem.Text = "Cadastrar estúdios";
            // 
            // editarCadastroDeEstúdiosToolStripMenuItem
            // 
            editarCadastroDeEstúdiosToolStripMenuItem.Name = "editarCadastroDeEstúdiosToolStripMenuItem";
            editarCadastroDeEstúdiosToolStripMenuItem.Size = new Size(406, 30);
            editarCadastroDeEstúdiosToolStripMenuItem.Text = "Editar cadastro de estúdios";
            // 
            // excluirEstúdiosToolStripMenuItem
            // 
            excluirEstúdiosToolStripMenuItem.Name = "excluirEstúdiosToolStripMenuItem";
            excluirEstúdiosToolStripMenuItem.Size = new Size(406, 30);
            excluirEstúdiosToolStripMenuItem.Text = "Excluir estúdios";
            // 
            // explorarEstúdiosToolStripMenuItem
            // 
            explorarEstúdiosToolStripMenuItem.Name = "explorarEstúdiosToolStripMenuItem";
            explorarEstúdiosToolStripMenuItem.Size = new Size(406, 30);
            explorarEstúdiosToolStripMenuItem.Text = "Explorar estúdios";
            // 
            // comunidadeToolStripMenuItem
            // 
            comunidadeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { discussõesToolStripMenuItem, mercadoToolStripMenuItem });
            comunidadeToolStripMenuItem.Name = "comunidadeToolStripMenuItem";
            comunidadeToolStripMenuItem.Size = new Size(157, 30);
            comunidadeToolStripMenuItem.Text = "Comunidade";
            // 
            // discussõesToolStripMenuItem
            // 
            discussõesToolStripMenuItem.Name = "discussõesToolStripMenuItem";
            discussõesToolStripMenuItem.Size = new Size(212, 30);
            discussõesToolStripMenuItem.Text = "Discussões";
            // 
            // mercadoToolStripMenuItem
            // 
            mercadoToolStripMenuItem.Name = "mercadoToolStripMenuItem";
            mercadoToolStripMenuItem.Size = new Size(212, 30);
            mercadoToolStripMenuItem.Text = "Mercado";
            // 
            // sobreToolStripMenuItem
            // 
            sobreToolStripMenuItem.Name = "sobreToolStripMenuItem";
            sobreToolStripMenuItem.Size = new Size(89, 30);
            sobreToolStripMenuItem.Text = "Sobre";
            // 
            // panel2
            // 
            panel2.Location = new Point(65, 56);
            panel2.Name = "panel2";
            panel2.Size = new Size(800, 400);
            panel2.TabIndex = 10;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(325, 71);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(287, 27);
            textBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Showcard Gothic", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(181, 72);
            label1.Name = "label1";
            label1.Size = new Size(138, 21);
            label1.TabIndex = 1;
            label1.Text = "Nome do jogo:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(325, 123);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(287, 27);
            textBox2.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Showcard Gothic", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(249, 129);
            label2.Name = "label2";
            label2.Size = new Size(70, 21);
            label2.TabIndex = 3;
            label2.Text = "Preço:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Showcard Gothic", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(212, 190);
            label3.Name = "label3";
            label3.Size = new Size(107, 21);
            label3.TabIndex = 5;
            label3.Text = "Categoria:";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(325, 183);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(287, 28);
            comboBox1.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Showcard Gothic", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(220, 249);
            label4.Name = "label4";
            label4.Size = new Size(99, 21);
            label4.TabIndex = 7;
            label4.Text = "Estúdios: ";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(325, 248);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(287, 28);
            comboBox2.TabIndex = 8;
            // 
            // button1
            // 
            button1.Font = new Font("Showcard Gothic", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(325, 299);
            button1.Name = "button1";
            button1.Size = new Size(214, 65);
            button1.TabIndex = 9;
            button1.Text = "Cadastrar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Showcard Gothic", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(220, 0);
            label5.Name = "label5";
            label5.Size = new Size(424, 37);
            label5.TabIndex = 11;
            label5.Text = "Cadastrado de jogo novo";
            // 
            // panel1
            // 
            panel1.Controls.Add(label5);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(comboBox2);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(textBox1);
            panel1.Location = new Point(62, 56);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 441);
            panel1.TabIndex = 1;
            panel1.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 553);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SenaiJogos";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem jogosToolStripMenuItem;
        private ToolStripMenuItem cadastrarNovoJogoToolStripMenuItem;
        private ToolStripMenuItem editrarCadastroDeJogoToolStripMenuItem;
        private ToolStripMenuItem explorarJogosToolStripMenuItem;
        private ToolStripMenuItem excluirJogosDaBibliotecaToolStripMenuItem;
        private ToolStripMenuItem novoJogoToolStripMenuItem;
        private ToolStripMenuItem novaCategoriaDeJogoToolStripMenuItem;
        private ToolStripMenuItem editarCadastroJogoToolStripMenuItem;
        private ToolStripMenuItem editarCategoriaDeJogoToolStripMenuItem;
        private ToolStripMenuItem usuáriosToolStripMenuItem;
        private ToolStripMenuItem cadastrarUsuáriosToolStripMenuItem;
        private ToolStripMenuItem editarUsuáriosToolStripMenuItem;
        private ToolStripMenuItem explorarUsuáriosToolStripMenuItem;
        private ToolStripMenuItem excluirUsuáriosToolStripMenuItem;
        private ToolStripMenuItem jogosToolStripMenuItem1;
        private ToolStripMenuItem categoriasToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem excluirCategoriaToolStripMenuItem;
        private ToolStripMenuItem estúdiosToolStripMenuItem;
        private ToolStripMenuItem cadastrarEstúdiosToolStripMenuItem;
        private ToolStripMenuItem editarCadastroDeEstúdiosToolStripMenuItem;
        private ToolStripMenuItem excluirEstúdiosToolStripMenuItem;
        private ToolStripMenuItem explorarEstúdiosToolStripMenuItem;
        private ToolStripMenuItem comunidadeToolStripMenuItem;
        private ToolStripMenuItem discussõesToolStripMenuItem;
        private ToolStripMenuItem mercadoToolStripMenuItem;
        private ToolStripMenuItem sobreToolStripMenuItem;
        private Panel panel2;
        private TextBox textBox1;
        private Label label1;
        private TextBox textBox2;
        private Label label2;
        private Label label3;
        private ComboBox comboBox1;
        private Label label4;
        private ComboBox comboBox2;
        private Button button1;
        private Label label5;
        private Panel panel1;
    }
}

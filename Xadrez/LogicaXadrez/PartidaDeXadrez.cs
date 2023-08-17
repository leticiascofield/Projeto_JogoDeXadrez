using System.Collections.Generic;
using TabuleiroXadrez;
using LogicaXadrez;
using System.Security.Authentication;

namespace LogicaXadrez {
    internal class PartidaDeXadrez {

        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get ; private set; }
        private HashSet<Peca> Peca;
        private HashSet<Peca> PecaCapturada;
        public bool Xeque { get; private set; }
        public Peca VulneravelEnPassant { get; private set; }

        public PartidaDeXadrez() {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Peca = new HashSet<Peca>();
            PecaCapturada = new HashSet<Peca>();
            Xeque = false;
            VulneravelEnPassant = null;
            AdicionarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino) {
            Peca peca = Tabuleiro.RemoverPeca(origem);
            peca.IncrementarQteMovimentos();
            Peca pecaCapturada = Tabuleiro.RemoverPeca(destino);
            Tabuleiro.AdicionarPeca(peca, destino);
            if (pecaCapturada != null) {
                PecaCapturada.Add(pecaCapturada);
            }

            // roque pequeno
            if (peca is Rei && destino.Coluna == origem.Coluna + 2) { 
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca Torre  = Tabuleiro.RemoverPeca(origemTorre);
                Torre.IncrementarQteMovimentos();
                Tabuleiro.AdicionarPeca(Torre, destinoTorre);
            }

            // roque grande
            if (peca is Rei && destino.Coluna == origem.Coluna - 2) {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca Torre = Tabuleiro.RemoverPeca(origemTorre);
                Torre.IncrementarQteMovimentos();
                Tabuleiro.AdicionarPeca(Torre, destinoTorre);
            }

            // en passant
            if (peca is Peao && origem.Coluna != destino.Coluna && pecaCapturada == null) {
                Posicao peaoCapturado;
                if(peca.Cor == Cor.Branca) {
                    peaoCapturado = new Posicao(destino.Linha + 1, destino.Coluna);
                } else {
                    peaoCapturado = new Posicao(destino.Linha - 1, destino.Coluna);
                }
                pecaCapturada = Tabuleiro.RemoverPeca(peaoCapturado);
                PecaCapturada.Add(pecaCapturada);
            }
            
            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) {
            Peca peca = Tabuleiro.RemoverPeca(destino);
            peca.DecrementarQteMovimentos();
            if (pecaCapturada != null) {
                Tabuleiro.AdicionarPeca(pecaCapturada, destino);
                PecaCapturada.Remove(pecaCapturada);
            }
            Tabuleiro.AdicionarPeca(peca, origem);

            // roque pequeno
            if (peca is Rei && destino.Coluna == origem.Coluna + 2) {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca Torre = Tabuleiro.RemoverPeca(destinoTorre);
                Torre.DecrementarQteMovimentos();
                Tabuleiro.AdicionarPeca(Torre, origemTorre);
            }

            // roque grande
            if (peca is Rei && destino.Coluna == origem.Coluna - 2) {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca Torre = Tabuleiro.RemoverPeca(destinoTorre);
                Torre.DecrementarQteMovimentos();
                Tabuleiro.AdicionarPeca(Torre, origemTorre);
            }

            // en passant
            if (peca is Peao && origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant) {
                Peca peao = Tabuleiro.RemoverPeca(destino);
                Posicao peaoCapturado;
                if (peca.Cor == Cor.Branca) {
                    peaoCapturado = new Posicao(3, destino.Coluna);
                } else {
                    peaoCapturado = new Posicao(4, destino.Coluna);
                }
                Tabuleiro.AdicionarPeca(peao, peaoCapturado);
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino) {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            if (EstaEmXeque(JogadorAtual)) {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Não é permitido colocar-se em xeque.");
            }
            if (EstaEmXeque(Oponente(JogadorAtual))) {
                Xeque = true;
            } else {
                Xeque = false;
            }

            Peca p = Tabuleiro.Peca(destino);

            // promocao
            if (p is Peao) {
                if ((p.Cor == Cor.Branca && destino.Linha == 0) || (p.Cor == Cor.Preta && destino.Linha == 7)) {
                    JogadaEspecialPromocao(destino);
                }
            }

            if (XequeMate(Oponente(JogadorAtual))) {
                Terminada = true;
            } else {
                Turno++;
                MudancaDeJogador();
            }

            // en passant
            if  (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2)) {
                VulneravelEnPassant = p;
            } else {
                VulneravelEnPassant = null;
            }
        }

        public void PosicaoDeOrigemValida(Posicao origem) {
            Peca aux = Tabuleiro.Peca(origem);
            if (aux == null) {
                throw new TabuleiroException("Não existe peça na posição escolhida.");
            }
            if (aux.Cor != JogadorAtual) {
                throw new TabuleiroException("A peça escolhida é do seu oponente.");
            }
            if (!aux.ExisteMovimentosPossiveis()) {
                throw new TabuleiroException("Não há movimentos possíveis para a peça escolhida.");
            }
        }

        public void PosicaoDeDestinoValida(Posicao origem, Posicao destino) {
            if (!Tabuleiro.Peca(origem).MovimentoPermitido(destino)) {
                throw new TabuleiroException("Esse movimento não é permitido.");
            }
        }

        private void MudancaDeJogador() {
            if (JogadorAtual == Cor.Branca) {
                JogadorAtual = Cor.Preta;
            } else {
                JogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> PecasCapturadasPorCor(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in PecaCapturada) {
                if (x.Cor == cor) {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogoPorCor(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Peca) {
                if (x.Cor == cor) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadasPorCor(cor));
            return aux;
        }

        private Cor Oponente(Cor cor) {
            if (cor == Cor.Branca) {
                return Cor.Preta;
            } else {
                return Cor.Branca;
            }
        }

        private Peca Rei(Cor cor) {
            foreach (Peca x in PecasEmJogoPorCor(cor)) {
                if(x is Rei) {
                    return x;
                }
            }
            return null;
        }

        private void JogadaEspecialPromocao (Posicao destino) { 
            Peca p = Tabuleiro.RemoverPeca(destino);
            Peca.Remove(p);

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("A jogada especial Promoção foi realizada!");
            Console.WriteLine("Digite por qual peça gostaria de trocar o seu peão:");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("(Opções: dama, torre, bispo, cavalo)");
            Console.ForegroundColor = ConsoleColor.White;
            Peca nova;
            string s = Console.ReadLine();

            while (s != "dama" && s != "torre" && s != "bispo" && s != "cavalo") {
                Console.WriteLine("Erro de digitação: certifique-se de digitar todas as letras minúsculas.");
                Console.Write("Reescreva: ");
                s = Console.ReadLine();
            }

            if (s == "dama") {
                nova = new Dama(p.Cor, Tabuleiro);
            } else if (s == "torre") {
                nova = new Torre(p.Cor, Tabuleiro);
            } else if (s == "bispo") {
                nova = new Bispo(p.Cor, Tabuleiro);
            } else if (s == "cavalo") {
                nova = new Cavalo(p.Cor, Tabuleiro);
            } else {
                nova = new Dama(p.Cor, Tabuleiro);
            }

            Tabuleiro.AdicionarPeca(nova, destino);
            Peca.Add(nova);
        }
            
        public bool EstaEmXeque(Cor cor) {
            Peca R = Rei(cor);
            if (R == null) {
                throw new TabuleiroException("Não existe rei da cor.");
            }
            foreach (Peca p in PecasEmJogoPorCor(Oponente(cor))) {
                bool[,] mat = p.MovimentosPossiveis();
                if(mat[R.Posicao.Linha, R.Posicao.Coluna] != false) {
                    return true;
                }
            }
            return false;
        }

        public bool XequeMate(Cor cor) {
            if (!EstaEmXeque(cor)) { 
                return false;
            }
            foreach (Peca p in PecasEmJogoPorCor(cor)) {
                bool[,] mat = p.MovimentosPossiveis();
                for (int i = 0; i < Tabuleiro.Linhas; i++) {
                    for (int j = 0; j < Tabuleiro.Colunas; j++) {
                        if (mat[i,j]) {
                            Posicao origem = p.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void AdicionarNovaPeca(char coluna, int linha, Peca peca) {
            Tabuleiro.AdicionarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Peca.Add(peca);
        }

        private void AdicionarPecas() {

            AdicionarNovaPeca('a', 1, new Torre(Cor.Branca, Tabuleiro));
            AdicionarNovaPeca('b', 1, new Cavalo(Cor.Branca, Tabuleiro));
            AdicionarNovaPeca('c', 1, new Bispo(Cor.Branca, Tabuleiro));
            AdicionarNovaPeca('d', 1, new Dama(Cor.Branca, Tabuleiro));
            AdicionarNovaPeca('e', 1, new Rei(Cor.Branca, Tabuleiro, this));
            AdicionarNovaPeca('f', 1, new Bispo(Cor.Branca, Tabuleiro));
            AdicionarNovaPeca('g', 1, new Cavalo(Cor.Branca, Tabuleiro));
            AdicionarNovaPeca('h', 1, new Torre(Cor.Branca, Tabuleiro));
            AdicionarNovaPeca('a', 2, new Peao(Cor.Branca, Tabuleiro, this));
            AdicionarNovaPeca('b', 2, new Peao(Cor.Branca, Tabuleiro, this));
            AdicionarNovaPeca('c', 2, new Peao(Cor.Branca, Tabuleiro, this));
            AdicionarNovaPeca('d', 2, new Peao(Cor.Branca, Tabuleiro, this));
            AdicionarNovaPeca('e', 2, new Peao(Cor.Branca, Tabuleiro, this));
            AdicionarNovaPeca('f', 2, new Peao(Cor.Branca, Tabuleiro, this));
            AdicionarNovaPeca('g', 2, new Peao(Cor.Branca, Tabuleiro, this));
            AdicionarNovaPeca('h', 2, new Peao(Cor.Branca, Tabuleiro, this));

            AdicionarNovaPeca('a', 8, new Torre(Cor.Preta, Tabuleiro));
            AdicionarNovaPeca('b', 8, new Cavalo(Cor.Preta, Tabuleiro));
            AdicionarNovaPeca('c', 8, new Bispo(Cor.Preta, Tabuleiro));
            AdicionarNovaPeca('d', 8, new Dama(Cor.Preta, Tabuleiro));
            AdicionarNovaPeca('e', 8, new Rei(Cor.Preta, Tabuleiro, this));
            AdicionarNovaPeca('f', 8, new Bispo(Cor.Preta, Tabuleiro));
            AdicionarNovaPeca('g', 8, new Cavalo(Cor.Preta, Tabuleiro));
            AdicionarNovaPeca('h', 8, new Torre(Cor.Preta, Tabuleiro));
            AdicionarNovaPeca('a', 7, new Peao(Cor.Preta, Tabuleiro, this));
            AdicionarNovaPeca('b', 7, new Peao(Cor.Preta, Tabuleiro, this));
            AdicionarNovaPeca('c', 7, new Peao(Cor.Preta, Tabuleiro, this));
            AdicionarNovaPeca('d', 7, new Peao(Cor.Preta, Tabuleiro, this));
            AdicionarNovaPeca('e', 7, new Peao(Cor.Preta, Tabuleiro, this));
            AdicionarNovaPeca('f', 7, new Peao(Cor.Preta, Tabuleiro, this));
            AdicionarNovaPeca('g', 7, new Peao(Cor.Preta, Tabuleiro, this));
            AdicionarNovaPeca('h', 7, new Peao(Cor.Preta, Tabuleiro, this));
        } 
    }
}

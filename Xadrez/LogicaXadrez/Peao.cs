using System.ComponentModel;
using TabuleiroXadrez;

namespace LogicaXadrez {
    internal class Peao : Peca {

        public Peao(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro) { }

        public override bool[,] MovimentosPossiveis() {
            bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao posicao = new Posicao(0, 0);
            Peca peca = Tabuleiro.Peca(Posicao.Linha, Posicao.Coluna);

            if (peca.Cor == Cor.Branca) {
                Posicao frente = new Posicao(Posicao.Linha - 1, Posicao.Coluna);

                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && MovimentoPossivel(posicao) && (!Tabuleiro.ExistePeca(frente))) {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }

                if (QteMovimentos == 0) {
                    posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                    if (Tabuleiro.PosicaoValida(posicao) && MovimentoPossivel(posicao) && (!Tabuleiro.ExistePeca(frente))) {
                        mat[posicao.Linha, posicao.Coluna] = true;
                    }
                }

                Posicao diagonal = new Posicao(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(diagonal)) {
                    Peca aux = Tabuleiro.Peca(Posicao.Linha - 1, Posicao.Coluna - 1);
                    if (aux != null && aux.Cor != peca.Cor) {
                        posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                        if (Tabuleiro.PosicaoValida(posicao) && MovimentoPossivel(posicao)) {
                            mat[posicao.Linha, posicao.Coluna] = true;
                        }
                    }
                }

                diagonal = new Posicao(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(diagonal)) {
                    Peca aux = Tabuleiro.Peca(Posicao.Linha - 1, Posicao.Coluna + 1);
                    if (aux != null && aux.Cor != peca.Cor) {
                        posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                        if (Tabuleiro.PosicaoValida(posicao) && MovimentoPossivel(posicao)) {
                            mat[posicao.Linha, posicao.Coluna] = true;
                        }
                    }
                }

            } else if (peca.Cor == Cor.Preta) {
                Posicao frente = new Posicao(Posicao.Linha + 1, Posicao.Coluna);

                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && MovimentoPossivel(posicao) && (!Tabuleiro.ExistePeca(frente))) {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }

                if (QteMovimentos == 0) {
                    posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                    if (Tabuleiro.PosicaoValida(posicao) && MovimentoPossivel(posicao) && (!Tabuleiro.ExistePeca(frente))) {
                        mat[posicao.Linha, posicao.Coluna] = true;
                    }
                }
                Posicao diagonal = new Posicao(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(diagonal)) {
                    Peca aux = Tabuleiro.Peca(Posicao.Linha + 1, Posicao.Coluna - 1);
                    if (aux != null && aux.Cor != peca.Cor) {
                        posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                        if (Tabuleiro.PosicaoValida(posicao) && MovimentoPossivel(posicao)) {
                            mat[posicao.Linha, posicao.Coluna] = true;
                        }
                    }

                }
                diagonal = new Posicao(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(diagonal)) {
                    Peca aux = Tabuleiro.Peca(Posicao.Linha + 1, Posicao.Coluna + 1);
                    if (aux != null && aux.Cor != peca.Cor) {
                        posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                        if (Tabuleiro.PosicaoValida(posicao) && MovimentoPossivel(posicao)) {
                            mat[posicao.Linha, posicao.Coluna] = true;
                        }
                    }
                }
            }

            return mat;
        }




        private bool MovimentoPossivel(Posicao posicao) {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override string ToString() {
            return "P";
        }
    }
}

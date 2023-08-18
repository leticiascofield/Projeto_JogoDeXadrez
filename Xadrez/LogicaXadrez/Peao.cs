using System.ComponentModel;
using TabuleiroXadrez;

namespace LogicaXadrez {
    internal class Peao : Peca {

        private PartidaDeXadrez PartidaDeXadrez;
        public Peao(Cor cor, Tabuleiro tabuleiro, PartidaDeXadrez partidaDeXadrez) : base(cor, tabuleiro) { 
            PartidaDeXadrez = partidaDeXadrez;
        }

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

                Posicao frente2 = new Posicao(Posicao.Linha - 2, Posicao.Coluna);
                if (QteMovimentos == 0) {
                    posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                    if (Tabuleiro.PosicaoValida(posicao) && MovimentoPossivel(posicao) && (!Tabuleiro.ExistePeca(frente)) && (!Tabuleiro.ExistePeca(frente2))) {
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

                // en passant
                if (Posicao.Linha == 3) {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tabuleiro.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && 
                        Tabuleiro.Peca(esquerda) == PartidaDeXadrez.VulneravelEnPassant) {
                        mat[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.PosicaoValida(direita) && ExisteInimigo(direita) &&
                        Tabuleiro.Peca(direita) == PartidaDeXadrez.VulneravelEnPassant) {
                        mat[direita.Linha -1, direita.Coluna] = true;
                    }
                }

            } else if (peca.Cor == Cor.Preta) {
                Posicao frente = new Posicao(Posicao.Linha + 1, Posicao.Coluna);

                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && MovimentoPossivel(posicao) && (!Tabuleiro.ExistePeca(frente))) {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }

                Posicao frente2 = new Posicao(Posicao.Linha + 2, Posicao.Coluna);
                if (QteMovimentos == 0) {
                    posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                    if (Tabuleiro.PosicaoValida(posicao) && MovimentoPossivel(posicao) && (!Tabuleiro.ExistePeca(frente)) && (!Tabuleiro.ExistePeca(frente2))) {
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

                // en passant
                if (Posicao.Linha == 4) {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tabuleiro.PosicaoValida(esquerda) && ExisteInimigo(esquerda) &&
                        Tabuleiro.Peca(esquerda) == PartidaDeXadrez.VulneravelEnPassant) {
                        mat[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.PosicaoValida(direita) && ExisteInimigo(direita) &&
                        Tabuleiro.Peca(direita) == PartidaDeXadrez.VulneravelEnPassant) {
                        mat[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
            }

            return mat;
        }


        private bool ExisteInimigo(Posicao posicao) {
            Peca p = Tabuleiro.Peca(posicao);
            return p != null && p.Cor != Cor;
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

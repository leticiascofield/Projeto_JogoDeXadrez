using TabuleiroXadrez;

namespace LogicaXadrez {
    internal class Rei : Peca {

        private PartidaDeXadrez PartidaDeXadrez;
        public Rei(Cor cor, Tabuleiro tabuleiro, PartidaDeXadrez partidaDeXadrez) : base(cor, tabuleiro) { 
            PartidaDeXadrez = partidaDeXadrez;
        }

        public override bool[,] MovimentosPossiveis() {
            bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao posicao = new Posicao(0,0);

            for (int i = -1; i <= 1; i++) {
                for (int j = -1; j <= 1; j++) {
                    if (!(i == 0 && j == 0)) {
                        posicao.DefinirValores(Posicao.Linha + i, Posicao.Coluna + j);
                        if (Tabuleiro.PosicaoValida(posicao) && MovimentoPossivel(posicao)) {
                            mat[posicao.Linha, posicao.Coluna] = true;
                        }
                    }
                }
            }

            // jogada especial roque
            if (QteMovimentos == 0 && !PartidaDeXadrez.Xeque) {
                // roque pequeno
                Posicao posicaoT1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
                if (TesteTorreParaRoque(posicaoT1)) {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);
                    if(Tabuleiro.Peca(p1) == null && Tabuleiro.Peca(p2) == null) {
                        mat[Posicao.Linha, Posicao.Coluna + 2] = true;
                    }
                }

                // roque grande
                Posicao posicaoT2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
                if (TesteTorreParaRoque(posicaoT2)) {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);
                    if (Tabuleiro.Peca(p1) == null && Tabuleiro.Peca(p2) == null && Tabuleiro.Peca(p3) == null) {
                        mat[Posicao.Linha, Posicao.Coluna - 2] = true;
                    }
                }
            }

            return mat;
        }

       
        private bool TesteTorreParaRoque(Posicao posicao) {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca != null && peca is Torre && peca.Cor == Cor && peca.QteMovimentos == 0;
        }

        private bool MovimentoPossivel(Posicao posicao) {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override string ToString() {
            return "R";
        }
    }
}

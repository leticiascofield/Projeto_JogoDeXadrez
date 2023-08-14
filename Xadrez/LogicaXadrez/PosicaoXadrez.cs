using TabuleiroXadrez;


namespace LogicaXadrez {
    internal class PosicaoXadrez {

        public int Linha { get; set; }
        public char Coluna { get; set; }

        public PosicaoXadrez(char coluna, int linha) {
            Linha = linha;
            Coluna = coluna;
        }

        public Posicao ToPosicao() {
            return new Posicao(8 - Linha, Coluna - 'a');
        }

        public override string ToString() {
            return "" + Coluna + Linha;
        }
    }
}

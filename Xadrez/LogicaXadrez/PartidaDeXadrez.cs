using TabuleiroXadrez;
using LogicaXadrez;

namespace LogicaXadrez {
    internal class PartidaDeXadrez {

        public Tabuleiro Tabuleiro { get; private set; }
        private int Turno;
        private Cor JogadorAtual;
        public bool Terminada { get ; private set; }

        public PartidaDeXadrez() {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            AdicionarPecas();
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino) {
            Peca peca = Tabuleiro.RemoverPeca(origem);
            peca.IncrementarQteMovimentos();
            Peca pecaCapturada = Tabuleiro.RemoverPeca(destino);
            Tabuleiro.AdicionarPeca(peca, destino);
        }

        private void AdicionarPecas() {
            Tabuleiro.AdicionarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('a', 1).ToPosicao());
            Tabuleiro.AdicionarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('h', 1).ToPosicao());
        }
    }
}

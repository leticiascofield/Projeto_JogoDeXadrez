using TabuleiroXadrez;
using LogicaXadrez;

namespace LogicaXadrez {
    internal class PartidaDeXadrez {

        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
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

        public void RealizaJogada(Posicao origem, Posicao destino) {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudancaDeJogador();
        }

        public void PosicaoDeOrigemValida(Posicao origem) {
            Peca aux = Tabuleiro.Peca(origem);
            if (aux == null) {
                throw new TabuleiroException("Não existe peça na posição escolhida.");
            }
            if (aux.Cor != JogadorAtual) {
                throw new TabuleiroException("A peça escolhida é do seu oponente.");
            }
            if (!aux.ExisteMovimentosPossivels()) {
                throw new TabuleiroException("Não há movimentos possíveis para a peça escolhida.");
            }
        }

        public void PosicaoDeDestinoValida(Posicao origem, Posicao destino) {
            if (!Tabuleiro.Peca(origem).MovimentoPermitido(destino)) {
                throw new TabuleiroException("Esse movimento não é permitido.");
            }
        }

        private void MudancaDeJogador() {
            if(JogadorAtual == Cor.Branca) {
                JogadorAtual = Cor.Preta;
            } else {
                JogadorAtual = Cor.Branca;
            }
        }

        private void AdicionarPecas() {
            Tabuleiro.AdicionarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('a', 2).ToPosicao());
            Tabuleiro.AdicionarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('b', 2).ToPosicao());
            Tabuleiro.AdicionarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('b', 1).ToPosicao());
            Tabuleiro.AdicionarPeca(new Rei(Cor.Branca, Tabuleiro), new PosicaoXadrez('a', 1).ToPosicao());
        }
    }
}

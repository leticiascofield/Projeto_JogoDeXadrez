using System;


namespace Xadrez.Tabuleiro {
    internal class Peca {

        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QteMovimentos { get; set; }
        public Tabuleiro Tabuleiro { get; set; }

        public Peca(Posicao posicao, Cor cor, Tabuleiro tabuleiro) {
            Posicao = posicao;
            Cor = cor;
            QteMovimentos = 0;
            Tabuleiro = tabuleiro;
        }
    }
}

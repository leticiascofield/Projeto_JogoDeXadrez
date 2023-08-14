﻿using TabuleiroXadrez;

namespace LogicaXadrez {
    internal class Torre : Peca {

        public Torre(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro) { }

        public override string ToString() {
            return "T";
        }
    }
}
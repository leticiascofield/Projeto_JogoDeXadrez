﻿using TabuleiroXadrez;

namespace LogicaXadrez {
    internal class Rei : Peca {

        public Rei(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro) { }

        public override string ToString() {
            return "R";
        }
    }
}

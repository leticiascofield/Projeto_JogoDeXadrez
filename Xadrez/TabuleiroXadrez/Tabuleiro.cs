﻿using System;
using System.Reflection.PortableExecutable;

namespace TabuleiroXadrez {
    internal class Tabuleiro {

        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] Pecas;

        public Tabuleiro(int linhas, int colunas) {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas];
        }


        public Peca Peca(int linha, int coluna) {
            return Pecas[linha, coluna];
        }

        public Peca Peca(Posicao posicao) {
            return Pecas[posicao.Linha, posicao.Coluna];
        }

        public bool ExistePeca(Posicao posicao) {
            ValidarPosicao(posicao);
            return Peca(posicao) != null;
        }
        public void AdicionarPeca(Peca peca, Posicao posicao) {
            if(ExistePeca(posicao)) {
                throw new TabuleiroException("Já existe uma peça nessa posição.");
            }
            Pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.Posicao = posicao;
        }

        public bool PosicaoValida(Posicao posicao) {
            if (posicao.Linha < 0 || posicao.Linha >= Linhas || posicao.Coluna < 0 || posicao.Coluna >= Colunas) {
                return false;
            } else return true;
        }

        public void ValidarPosicao(Posicao posicao) {
            if(!PosicaoValida(posicao)) {
                throw new TabuleiroException("Posição inválida.");
            }
        }
    }
}
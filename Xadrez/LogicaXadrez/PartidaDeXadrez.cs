﻿using System.Collections.Generic;
using TabuleiroXadrez;
using LogicaXadrez;

namespace LogicaXadrez {
    internal class PartidaDeXadrez {

        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get ; private set; }
        private HashSet<Peca> Peca;
        private HashSet<Peca> PecaCapturada;
        public bool Xeque { get; private set; }

        public PartidaDeXadrez() {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Peca = new HashSet<Peca>();
            PecaCapturada = new HashSet<Peca>();
            Xeque = false;
            AdicionarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino) {
            Peca peca = Tabuleiro.RemoverPeca(origem);
            peca.IncrementarQteMovimentos();
            Peca pecaCapturada = Tabuleiro.RemoverPeca(destino);
            Tabuleiro.AdicionarPeca(peca, destino);
            if(pecaCapturada != null) {
                PecaCapturada.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) {
            Peca peca = Tabuleiro.RemoverPeca(destino);
            peca.DecrementarQteMovimentos();
            if(pecaCapturada != null) {
                Tabuleiro.AdicionarPeca(pecaCapturada, destino);
                PecaCapturada.Remove(pecaCapturada);
            }
            Tabuleiro.AdicionarPeca(peca, origem); 
        }

        public void RealizaJogada(Posicao origem, Posicao destino) {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            if(EstaEmXeque(JogadorAtual)) {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Não é permitido colocar-se em xeque.");
            }
            if(EstaEmXeque(Oponente(JogadorAtual))) {
                Xeque = true;
            } else {
                Xeque = false;
            }
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

        public HashSet<Peca> PecasCapturadasPorCor(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in PecaCapturada) {
                if (x.Cor == cor) {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogoPorCor(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Peca) {
                if (x.Cor == cor) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadasPorCor(cor));
            return aux;
        }

        private Cor Oponente(Cor cor) {
            if (cor == Cor.Branca) {
                return Cor.Preta;
            } else {
                return Cor.Branca;
            }
        }

        private Peca Rei(Cor cor) {
            foreach (Peca x in PecasEmJogoPorCor(cor)) {
                if(x is Rei) {
                    return x;
                }
            }
            return null;
        }
            
        public bool EstaEmXeque(Cor cor) {
            Peca R = Rei(cor);
            if(R == null) {
                throw new TabuleiroException("Não existe rei da cor.");
            }
            foreach(Peca x in PecasEmJogoPorCor(Oponente(cor))) {
                bool[,] mat = x.MovimentosPossiveis();
                if(mat[R.Posicao.Linha, R.Posicao.Coluna] != false) {
                    return true;
                }
            }
            return false;
        }

        public void AdicionarNovaPeca(char coluna, int linha, Peca peca) {
            Tabuleiro.AdicionarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Peca.Add(peca);
        }

        private void AdicionarPecas() {

            AdicionarNovaPeca('a', 1, new Torre(Cor.Branca, Tabuleiro));
            AdicionarNovaPeca('h', 1, new Torre(Cor.Branca, Tabuleiro));
            AdicionarNovaPeca('e', 1, new Rei(Cor.Branca, Tabuleiro));

            AdicionarNovaPeca('a', 8, new Torre(Cor.Preta, Tabuleiro));
            AdicionarNovaPeca('h', 8, new Torre(Cor.Preta, Tabuleiro));
            AdicionarNovaPeca('d', 8, new Rei(Cor.Preta, Tabuleiro));
            
        }
    }
}

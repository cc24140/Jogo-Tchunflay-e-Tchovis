# 🎮 Jogo Tchunflay e Tchóvis

## Sobre o Projeto

**Jogo Tchunflay e Tchóvis** é um jogo de luta 2D desenvolvido na **Unity**, onde dois jogadores competem localmente em batalhas rápidas utilizando golpes físicos e ataques especiais.

Os jogadores podem escolher entre os personagens **Tchunflay** e **Tchóvis**, cada um controlado por um conjunto próprio de teclas. O objetivo é reduzir a vida do adversário a zero antes do fim do tempo ou terminar a luta com mais vida que o oponente.

---

## Funcionalidades

* ✅ Seleção de personagens para dois jogadores
* ✅ Sistema de movimentação lateral
* ✅ Sistema de pulo
* ✅ Ataques de soco
* ✅ Ataques de chute
* ✅ Ataque especial (Hadouken)
* ✅ Sistema de vida
* ✅ Cronômetro de partida
* ✅ Sistema de pausa
* ✅ Tela de vitória
* ✅ Efeitos sonoros
* ✅ Música de fundo

---

## Tecnologias Utilizadas

* **Unity 6000.3.2f1**
* **C#**
* **Unity UI**
* **TextMeshPro**
* **Physics 2D**

---

## Controles

### Jogador 1

| Ação                | Tecla |
| ------------------- | ----- |
| Mover para esquerda | A     |
| Mover para direita  | D     |
| Pular               | W     |
| Soco                | Q     |
| Chute               | E     |
| Hadouken            | R     |

### Jogador 2

| Ação                | Tecla |
| ------------------- | ----- |
| Mover para esquerda | ←     |
| Mover para direita  | →     |
| Pular               | ↑     |
| Soco                | J     |
| Chute               | K     |
| Hadouken            | L     |

### Controles Gerais

| Ação               | Tecla  |
| ------------------ | ------ |
| Pausar / Continuar | Espaço |

---

## Mecânicas do Jogo

### Sistema de Vida

Cada personagem inicia a luta com **100 pontos de vida**.

| Golpe    | Dano |
| -------- | ---- |
| Soco     | 10   |
| Chute    | 15   |
| Hadouken | 10   |

Quando a vida de um jogador chega a zero, ocorre um **K.O.**, encerrando a partida imediatamente.

### Sistema de Tempo

Cada luta possui duração máxima de **90 segundos**.

Ao final do tempo:

* Vence o jogador com mais vida restante.
* Se ambos possuírem a mesma quantidade de vida, a luta termina em empate.

---

## Estrutura dos Scripts

| Script                 | Função                                   |
| ---------------------- | ---------------------------------------- |
| `ControleJogador.cs`   | Movimentação, combate, vida e ataques    |
| `GerenciadorLuta.cs`   | Controle geral da luta                   |
| `GerenciadorPause.cs`  | Sistema de pausa                         |
| `HadukenScript.cs`     | Controle dos projéteis                   |
| `SeletorPersonagem.cs` | Seleção de personagens                   |
| `DadosDoJogo.cs`       | Armazenamento das escolhas dos jogadores |
| `LoadScene.cs`         | Gerenciamento de troca de cenas          |

---

## Como Executar

1. Clone o repositório:

```bash
git clone https://github.com/cc24140/Jogo-Tchunflay-e-Tchovis.git
```

2. Abra o projeto utilizando a **Unity 6000.3.2f1**.

3. Carregue a cena principal do menu.

4. Clique em **Play** dentro da Unity.

5. Escolha os personagens e inicie a luta.

---

## Estrutura do Projeto

```text
Assets/
├── Animations/
│   └── Poeira/
│
├── Audio/
│   ├── fim.wav
│   ├── golpe.mp3
│   ├── lancarHadouken.mp3
│   ├── luta.wav
│   ├── menu.wav
│   ├── recebeHadouken.mp3
│   └── tomaDano.mp3
│
├── Materials/
│   └── SemAtrito.physicsMaterial2D
│
├── Prefabs/
│   ├── HadukenProjectile.prefab
│   └── Poeira Prefab.prefab
│
├── Scenes/
│   ├── MainMenu.unity
│   └── CenaLuta.unity
│
├── Scripts/
│   ├── ControleJogador.cs
│   ├── DadosDoJogo.cs
│   ├── GerenciadorLuta.cs
│   ├── GerenciadorPause.cs
│   ├── HadukenScript.cs
│   ├── LoadScene.cs
│   └── SeletorPersonagem.cs
│
├── Sprites/
│   ├── Personagens
│   ├── Hadouken
│   ├── Interface
│   ├── Cenário
│   └── Efeitos Visuais
│
└── TextMesh Pro/
```

---

## Melhorias Futuras

* Novos personagens jogáveis
* Sistema de combos
* Mais ataques especiais
* Modo contra IA
* Multiplayer online
* Novos cenários
* Sistema de ranking

---

## Desenvolvedores

* **Mariana Marietti da Costa** — RA 24140
* **Murilo Matos Lopes** — RA 24145
* **Rafaelly Maria Nascimento da Silva** — RA 24153

---

## Licença

Este projeto foi desenvolvido para fins acadêmicos e educacionais.

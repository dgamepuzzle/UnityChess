﻿using System.Collections.Generic;

namespace UnityChess {
	public class Bishop : Piece {
		private static int instanceCounter;

		public Bishop(Square startingPosition, Side pieceOwner) : base(startingPosition, pieceOwner) {
			ID = ++instanceCounter;
		}

		private Bishop(Bishop bishopCopy) : base(bishopCopy) {
			ID = bishopCopy.ID;
		}

		public override void UpdateValidMoves(Board board, LinkedList<Turn> previousMoves) {
			ValidMoves.Clear();
			CheckDiagonalDirections(board);
		}

		private void CheckDiagonalDirections(Board board) {
			foreach (int fileOffset in new[] {-1, 1}) {
				foreach (int rankOffset in new[] {-1, 1}) {
					Square testSquare = new Square(Position, fileOffset, rankOffset);
					Movement testMove = new Movement(Position, testSquare);

					while (testSquare.IsValid()) {
						Square enemyKingPosition = PieceOwner == Side.White ? board.BlackKing.Position : board.WhiteKing.Position;
						if (testSquare.IsOccupied(board)) {
							if (!testSquare.IsOccupiedBySide(board, PieceOwner) && Rules.MoveObeysRules(board, testMove, PieceOwner) && testSquare != enemyKingPosition)
								ValidMoves.Add(new Movement(testMove));

							break;
						}

						if (Rules.MoveObeysRules(board, testMove, PieceOwner) && testSquare != enemyKingPosition)
							ValidMoves.Add(new Movement(testMove));

						testSquare = new Square(testSquare, fileOffset, rankOffset);
						testMove = new Movement(Position, testSquare);
					}
				}
			}
		}

		public override Piece Clone() => new Bishop(this);
	}
}
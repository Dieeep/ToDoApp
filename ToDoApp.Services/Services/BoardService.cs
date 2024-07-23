using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services.Services
{
    public class BoardService : IBoardService
    {
        private readonly ToDoContext _context;

        public BoardService(ToDoContext context)
        {
            _context = context;
        }
        public async Task<Board> CreateBoardAsync(Board board)
        {
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();
            return board;
        }

        public async Task<Board> GetBoardAsync(int id)
        {
            return await _context.Boards.FindAsync(id);
        }
    }
}

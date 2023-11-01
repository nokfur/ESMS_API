﻿using ESMS_Data.Models;
using ESMS_Data.Repositories.RepositoryBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESMS_Data.Repositories.ParticipationRepository
{
    public class ParticipationRepository : RepositoryBase<Participation>, IParticipationRepository
    {
        private ESMSContext _context;
        private DbSet<Participation> _participations;
        private DbSet<User> _users;
        private DbSet<Room> _rooms;

        public ParticipationRepository(ESMSContext context) : base(context)
        {
            _context = context;
            _participations = _context.Set<Participation>();
            _users = _context.Set<User>();
            _rooms = _context.Set<Room>();
        }

        public async Task<object> GetStudentListInExam(int idt, string subject, string room)
        {
            var studentUserNames = await _participations.Where(p => p.Idt == idt
                                                     && p.SubjectId.Equals(subject)
                                                     && p.RoomNumber.Equals(room))
                                            .Select(p => p.UserName)
                                            .ToListAsync();

            var students = _users.Where(u => studentUserNames.Contains(u.UserName))
                                 .Select(u => new
                                 {
                                     u.UserName,
                                     u.Name,
                                     u.RollNumber
                                 });

            return await students.ToListAsync();
        }

        public async Task<int?> GetRoomCapacity(string room)
        {
            return await _rooms.Where(r => r.Number.Equals(room))
                                .Select(r => r.Capacity)
                                .FirstAsync();
        }

        public async Task<int> GetTotalStudentInRoom(int idt, string room)
        {
            var qr = _participations.Where(p => p.Idt == idt && p.RoomNumber.Equals(room));

            return await qr.CountAsync();
        }

        public async Task<List<Participation>> GetParticipationsOnList(int idt, string subject, string room, List<string> students)
        {
            return await _participations
                                        .Where(p => p.Idt == idt
                                                && p.SubjectId.Equals(subject)
                                                && p.RoomNumber.Equals(room)
                                                && students.Contains(p.UserName))
                                        .ToListAsync();
        }
    }
}
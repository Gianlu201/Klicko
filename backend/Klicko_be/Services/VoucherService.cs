using Klicko_be.Data;
using Klicko_be.DTOs.Voucher;
using Klicko_be.Models;
using Microsoft.EntityFrameworkCore;

namespace Klicko_be.Services
{
    public class VoucherService
    {
        private readonly ApplicationDbContext _context;

        public VoucherService(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task<bool> TrySaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Voucher>?> GetAllUserVouchersAsync(string userId)
        {
            try
            {
                var vouchers = await _context
                    .Vouchers.Include(v => v.Category)
                    .Include(v => v.User)
                    .OrderByDescending(v => v.ReservationDate)
                    .Where(v => v.UserId == userId)
                    .ToListAsync();

                return vouchers;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Voucher?> GetVoucherByCodeAsync(string voucherCode)
        {
            try
            {
                var voucher = await _context
                    .Vouchers.Include(v => v.Category)
                    .Include(v => v.User)
                    .Where(v => !v.IsUsed)
                    .FirstOrDefaultAsync(v => v.VoucherCode == voucherCode);

                return voucher;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> EditVoucherByIdAsync(
            Guid voucherId,
            EditVoucherRequestDto editVoucher,
            string userId
        )
        {
            try
            {
                var voucher = await _context.Vouchers.FindAsync(voucherId);

                if (voucher == null)
                {
                    return false;
                }

                // controlla se il voucher ha una data di prenotazione e se presente verifica
                // che manchino almeno 2 giorni, altrimenti non fa modificare il voucher

                // se invece il voucer non ha una data di prenotazione, allora vuol dire che
                // deve essere riscattato e controlla che la data di prenotazione sia ad
                // almeno 7 giorni a partire dal giorno della riscossione e che la prenotazione
                // ricada all'interno del tempo di validità del voucer

                // se la richiesta di modifica ricade nelle limitazioni imposte allora rende
                // impossibile modificare il voucher

                if (
                    voucher.ReservationDate != null
                    && voucher.ReservationDate <= DateTime.Today.AddDays(2)
                )
                {
                    return false;
                }
                else if (
                    voucher.ReservationDate == null
                    && editVoucher.ReservationDate != null
                    && editVoucher.ReservationDate <= DateTime.Today.AddDays(7)
                    && editVoucher.ReservationDate > voucher.ExpirationDate
                )
                {
                    return false;
                }

                if (editVoucher.IsUsed)
                {
                    voucher.IsUsed = true;
                    voucher.ReservationDate = editVoucher.ReservationDate;
                    voucher.UserId = userId;
                }
                else
                {
                    voucher.IsUsed = false;
                    voucher.ReservationDate = null;
                    voucher.UserId = null;
                }

                return await TrySaveAsync();
            }
            catch
            {
                return false;
            }
        }
    }
}

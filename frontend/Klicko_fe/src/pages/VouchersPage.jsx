import { Ticket, TicketCheck } from 'lucide-react';
import React, { useState } from 'react';
import Button from '../components/ui/Button';

const VouchersPage = () => {
  const [redeemVoucherOption, setRedeemVoucherOption] = useState(true);
  const [redeemedVouchers, setRedeemedVouchers] = useState([]);
  const [voucherCodeSearch, setVoucherCodeSearch] = useState('');

  return (
    <div className='max-w-7xl mx-auto min-h-screen'>
      <h1 className='text-4xl font-bold mt-10 mb-3'>Utilizza il tuo Voucher</h1>
      <p className='text-gray-500 font-normal mb-8'>
        Utilizza il codice del tuo voucher per riscattare o prenotare la tua
        esperienza
      </p>

      <div className='max-w-3xl mx-auto'>
        <div className='bg-gray-100 rounded-lg p-1 flex justify-center items-center mb-6'>
          <button
            className={`text-sm font-medium rounded-sm grow p-1 cursor-pointer ${
              redeemVoucherOption && 'bg-white'
            }`}
            onClick={() => {
              setRedeemVoucherOption(true);
            }}
          >
            Riscatta Voucher
          </button>
          <button
            className={`text-sm font-medium rounded-sm grow p-1 cursor-pointer ${
              !redeemVoucherOption && 'bg-white'
            }`}
            onClick={() => {
              setRedeemVoucherOption(false);
            }}
          >
            I miei Voucher ({redeemedVouchers.length})
          </button>
        </div>

        <div className='bg-white rounded-lg border border-gray-400/10 p-5 shadow'>
          {redeemVoucherOption ? (
            <div>
              <div className='flex justify-start items-center gap-2 mb-1.5'>
                <Ticket className='text-primary pt-0.5' />
                <h4 className='text-2xl font-semibold'>
                  Riscatta il tuo Voucher
                </h4>
              </div>

              <p className='text-gray-500 text-sm mb-6'>
                Inserisci il codice del tuo voucher ricevuto via email o tramite
                cofanetto regalo
              </p>

              <p className='text-gray-500 font-bold text-sm mb-2'>
                Codice Voucher
              </p>
              <form className='flex justify-center items-center gap-2 mb-4'>
                <input
                  type='text'
                  className='grow bg-background border border-gray-400/20 rounded-md px-3 py-2'
                  placeholder='Es. ABCD1234-EFGH5678-IJKL1234'
                  value={voucherCodeSearch}
                  onChange={(e) => {
                    setVoucherCodeSearch(e.target.value);
                  }}
                />
                <Button
                  variant='primary'
                  disabled={voucherCodeSearch.length < 26 ? true : false}
                >
                  Cerca
                </Button>
              </form>
            </div>
          ) : (
            <div>
              <div className='flex justify-start items-center gap-2 mb-1.5'>
                <TicketCheck className='text-primary pt-0.5' />
                <h4 className='text-2xl font-semibold'>I miei Voucher</h4>
              </div>

              <p className='text-gray-500 text-sm mb-6'>
                Visualizza tutti i voucher delle esperienze che hai prenotato
              </p>

              {redeemedVouchers.length > 0 ? (
                <div></div>
              ) : (
                <div className='py-4'>
                  <p className='text-gray-500 font-medium text-center mb-2'>
                    Non hai ancora nessun voucher prenotato
                  </p>
                  <p className='text-gray-500 font-medium text-center text-sm'>
                    Puoi prenotare un'esperienza con un voucher utilizzando il
                    codice ricevuto nel cofanetto regalo
                  </p>
                </div>
              )}
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default VouchersPage;

import { CircleAlert, Ticket, TicketCheck } from 'lucide-react';
import React, { useEffect, useState } from 'react';
import Button from '../components/ui/Button';
import { useNavigate } from 'react-router-dom';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import { toast } from 'sonner';
import { Modal, ModalBody, ModalHeader } from 'flowbite-react';

const VouchersPage = () => {
  const [redeemVoucherOption, setRedeemVoucherOption] = useState(true);
  const [redeemedVouchers, setRedeemedVouchers] = useState([]);
  const [voucherCodeSearch, setVoucherCodeSearch] = useState('');
  const [searchedVoucher, setSearchedVoucher] = useState(null);
  const [voucherNotFound, setVoucherNotFound] = useState(false);
  const [reservationDate, setReservationDate] = useState(null);

  const [openConfermeModal, setOpenConfermeModal] = useState(false);
  const [selectedVoucher, setSelectedVoucher] = useState(null);

  const navigate = useNavigate();

  const getUserVouchers = async () => {
    try {
      let tokenObj = localStorage.getItem('klicko_token');

      if (!tokenObj) {
        navigate('/login');
      }

      let token = JSON.parse(tokenObj).token;

      const response = await fetch(
        'https://localhost:7235/api/Voucher/getAllUserVouchers',
        {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${token}`,
          },
        }
      );

      if (response.ok) {
        const data = await response.json();

        setRedeemedVouchers(data.vouchers);
      } else {
        throw new Error('Errore nel salvataggio!');
      }
    } catch (e) {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>Qualcosa è andato storto, riprovare</p>
        </>
      );
      console.log(e.message);
    }
  };

  const getVoucherByCode = async () => {
    try {
      setVoucherNotFound(false);
      setSearchedVoucher(null);

      let tokenObj = localStorage.getItem('klicko_token');

      if (!tokenObj) {
        navigate('/login');
      }

      let token = JSON.parse(tokenObj).token;

      const response = await fetch(
        `https://localhost:7235/api/Voucher/getVoucherByCode/${voucherCodeSearch.toUpperCase()}`,
        {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${token}`,
          },
        }
      );

      if (response.ok) {
        const data = await response.json();

        setSearchedVoucher(data.voucher);
        setVoucherCodeSearch('');
      } else {
        setVoucherNotFound(true);
      }
    } catch (e) {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>Qualcosa è andato storto, riprovare</p>
        </>
      );
      console.log(e.message);
    }
  };

  const sendReservationVoucherForm = async (e) => {
    try {
      e.preventDefault();

      let tokenObj = localStorage.getItem('klicko_token');

      if (!tokenObj) {
        navigate('/login');
      }

      let token = JSON.parse(tokenObj).token;

      const body = {
        isUsed: true,
        reservationDate: reservationDate,
      };

      const response = await fetch(
        `https://localhost:7235/api/Voucher/editVoucher/${searchedVoucher.voucherId}`,
        {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${token}`,
          },
          body: JSON.stringify(body),
        }
      );

      if (response.ok) {
        toast.success(
          <>
            <p className='font-bold'>Voucher riscosso!</p>
            <p>Prenotazione avvenuta con successo</p>
          </>
        );

        setReservationDate(null);
        setSearchedVoucher(null);
        setVoucherCodeSearch('');
        getUserVouchers();
      } else {
        toast.error(
          <>
            <p className='font-bold'>Errore!</p>
            <p>Qualcosa è andato storto, riprovare</p>
          </>
        );
      }
    } catch (e) {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>Qualcosa è andato storto, riprovare</p>
        </>
      );
      console.log(e.message);
    }
  };

  const removeReservation = async (voucherId) => {
    try {
      let tokenObj = localStorage.getItem('klicko_token');

      if (!tokenObj) {
        navigate('/login');
      }

      let token = JSON.parse(tokenObj).token;

      const body = {
        isUsed: false,
      };

      const response = await fetch(
        `https://localhost:7235/api/Voucher/editVoucher/${voucherId}`,
        {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${token}`,
          },
          body: JSON.stringify(body),
        }
      );

      if (response.ok) {
        toast.success(
          <>
            <p className='font-bold'>Cancellazione effettuata!</p>
            <p>La prenotazione è stata cancellata</p>
          </>
        );
        getUserVouchers();
      } else {
        toast.error(
          <>
            <p className='font-bold'>Errore!</p>
            <p>Qualcosa è andato storto, riprovare</p>
          </>
        );
      }
    } catch (e) {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>Qualcosa è andato storto, riprovare</p>
        </>
      );
      console.log(e.message);
    }
  };

  const getMinDate = () => {
    const today = new Date();
    today.setDate(today.getDate() + 7);
    return today;
  };

  const isWeekday = (date) => {
    const day = date.getDay();
    return day !== 0 && day !== 6;
  };

  const checkIsPassed = (reservationDate) => {
    return new Date(reservationDate) - new Date() <= 0;
  };

  useEffect(() => {
    getUserVouchers();
  }, []);

  return (
    <div className='max-w-7xl mx-auto min-h-screen px-6 xl:px-0'>
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

        <div className='bg-white rounded-lg border border-gray-400/10 p-5 shadow mb-16'>
          {redeemVoucherOption ? (
            // Riscatta Voucher
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
              <form
                className='flex justify-center items-center gap-2 mb-4'
                onSubmit={(e) => {
                  e.preventDefault();
                }}
              >
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
                  disabled={voucherCodeSearch.length !== 26 ? true : false}
                  onClick={() => {
                    getVoucherByCode();
                  }}
                >
                  Cerca
                </Button>
              </form>

              {voucherNotFound && (
                <div className='bg-red-200/60 px-4 py-3 mx-3 rounded-lg '>
                  <p className='text-gray-600'>
                    Il codice inserito non corrisponde a nessun voucher o il
                    voucher cercato è già stato riscattato
                  </p>
                </div>
              )}

              {searchedVoucher !== null && (
                <div className='border border-gray-400/30 rounded-lg px-5 py-6'>
                  <h4 className='text-xl mb-2'>{searchedVoucher.title}</h4>
                  <p className='text-sm text-gray-500 mb-4'>
                    Scegli una data per prenotare la tua esperienza
                  </p>

                  <form
                    onSubmit={(e) => {
                      sendReservationVoucherForm(e);
                    }}
                    className='w-full'
                  >
                    <label className='text-sm font-semibold block mb-1'>
                      Data desiderata
                    </label>

                    <DatePicker
                      selected={reservationDate}
                      onChange={(date) => setReservationDate(date)}
                      minDate={getMinDate()}
                      filterDate={isWeekday}
                      placeholderText='Seleziona una data (solo lun-ven)'
                      dateFormat='dd/MM/yyyy'
                      className='w-full bg-background border border-gray-400/30 rounded-lg px-4 py-2 mb-2'
                      wrapperClassName='w-full'
                    />

                    <p className='text-sm text-gray-500 mb-5'>
                      Disponibile dal lunedì al venerdì, con prenotazione minimo
                      7 giorni in anticipo
                    </p>

                    <Button
                      type='submit'
                      variant='primary'
                      fullWidth={true}
                      disabled={reservationDate === null}
                    >
                      Conferma prenotazione
                    </Button>
                  </form>
                </div>
              )}
            </div>
          ) : (
            // I miei Voucher
            <div>
              <div className='flex justify-start items-center gap-2 mb-1.5'>
                <TicketCheck className='text-primary pt-0.5' />
                <h4 className='text-2xl font-semibold'>I miei Voucher</h4>
              </div>

              <p className='text-gray-500 text-sm mb-6'>
                Visualizza tutti i voucher delle esperienze che hai prenotato
              </p>

              {redeemedVouchers.length > 0 ? (
                <div>
                  {redeemedVouchers.map((voucher) => (
                    <div
                      key={voucher.voucherId}
                      className={`border border-gray-400/40 rounded-lg p-6 mb-6 mx-1 ${
                        checkIsPassed(voucher.reservationDate) && 'bg-gray-100'
                      }`}
                    >
                      <div className='flex justify-between items-center gap-4 mb-2'>
                        <h4 className='text-xl'>{voucher.title}</h4>
                        <span
                          className={`text-xs  rounded-md px-2 py-1 ${
                            checkIsPassed(voucher.reservationDate)
                              ? 'bg-gray-300'
                              : 'bg-green-200 text-gray-600'
                          }`}
                        >
                          Prenotato
                        </span>
                      </div>

                      <p className='text-xs mb-3'>{voucher.voucherCode}</p>

                      <p className='text-sm text-gray-500 mb-3'>
                        Prenotato per:{' '}
                        {new Date(voucher.reservationDate).toLocaleDateString(
                          'it-IT',
                          {
                            year: 'numeric',
                            month: 'long',
                            day: 'numeric',
                          }
                        )}
                      </p>

                      {new Date(voucher.reservationDate) - Date.now() >
                        2 * 24 * 60 * 60 * 1000 && (
                        <Button
                          variant='danger-outline'
                          size='sm'
                          onClick={() => {
                            setSelectedVoucher(voucher);
                            setOpenConfermeModal(true);
                          }}
                        >
                          Cancella prenotazione
                        </Button>
                      )}
                    </div>
                  ))}
                </div>
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

              {/* Modale conferma */}
              {selectedVoucher != null && (
                <Modal
                  show={openConfermeModal}
                  size='md'
                  onClose={() => {
                    setSelectedVoucher(null);
                    setOpenConfermeModal(false);
                  }}
                  popup
                >
                  <ModalHeader className='bg-background rounded-t-2xl' />
                  <ModalBody className='bg-background rounded-b-2xl'>
                    <div className='text-center'>
                      <CircleAlert className='mx-auto mb-4 h-14 w-14 text-red-500' />
                      <h3 className='mb-5 text-lg font-medium'>
                        Sicuro di voler eliminare questa prenotazione?
                      </h3>
                      <p className='mb-8 text-gray-500'>
                        La prenotazione sarà cancellata e potrai effettuarne
                        un'altra utilizzando il codice{' '}
                        <b>{selectedVoucher.voucherCode}</b> entro la data di
                        validità dello stesso
                      </p>
                      <div className='flex justify-center gap-4'>
                        <Button
                          variant='danger'
                          onClick={() => {
                            removeReservation(selectedVoucher.voucherId);
                            setSelectedVoucher(null);
                            setOpenConfermeModal(false);
                          }}
                        >
                          Elimina
                        </Button>
                        <Button
                          color='gray'
                          onClick={() => {
                            setSelectedVoucher(null);
                            setOpenConfermeModal(false);
                          }}
                        >
                          Annulla
                        </Button>
                      </div>
                    </div>
                  </ModalBody>
                </Modal>
              )}
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default VouchersPage;

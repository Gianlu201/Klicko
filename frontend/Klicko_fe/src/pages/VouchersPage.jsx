import { CircleAlert, Ticket, TicketCheck } from 'lucide-react';
import React, { useEffect, useState } from 'react';
import Button from '../components/ui/Button';
import { useNavigate } from 'react-router-dom';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import { toast } from 'sonner';
import { Modal, ModalBody, ModalHeader } from 'flowbite-react';
import SkeletonList from '../components/ui/SkeletonList';
import Spinner from '../components/ui/Spinner';
import SkeletonText from '../components/ui/SkeletonText';

const VouchersPage = () => {
  const backendUrl = import.meta.env.VITE_BACKEND_URL;

  const [isLoading, setIsLoading] = useState(true);
  const [isSearchingVoucher, setIsSearchingVoucher] = useState(false);
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
      setIsLoading(true);

      let tokenObj = localStorage.getItem('klicko_token');

      if (tokenObj === null) {
        navigate('/login');
      } else {
        let token = JSON.parse(tokenObj).token;

        const response = await fetch(
          `${backendUrl}/Voucher/getAllUserVouchers`,
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

          setIsLoading(false);
          setRedeemedVouchers(data.vouchers);
        } else {
          throw new Error('Errore nel recupero dei dati!');
        }
      }
    } catch (e) {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>{e.message}</p>
        </>
      );

      navigate('/');
    }
  };

  const getVoucherByCode = async () => {
    try {
      setIsSearchingVoucher(true);

      setVoucherNotFound(false);
      setSearchedVoucher(null);

      let tokenObj = localStorage.getItem('klicko_token');

      if (!tokenObj) {
        navigate('/login');
      }

      let token = JSON.parse(tokenObj).token;

      const response = await fetch(
        `${backendUrl}/Voucher/getVoucherByCode/${voucherCodeSearch.toUpperCase()}`,
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

        setIsSearchingVoucher(false);
        setSearchedVoucher(data.voucher);
        setVoucherCodeSearch('');
      } else {
        setVoucherNotFound(true);
      }
    } catch {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>Qualcosa è andato storto, riprovare</p>
        </>
      );
      setIsSearchingVoucher(false);
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
        `${backendUrl}/Voucher/editVoucher/${searchedVoucher.voucherId}`,
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
        throw new Error();
      }
    } catch {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>Qualcosa è andato storto, riprovare</p>
        </>
      );
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
        `${backendUrl}/Voucher/editVoucher/${voucherId}`,
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
        throw new Error();
      }
    } catch {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>Qualcosa è andato storto, riprovare</p>
        </>
      );
    }
  };

  const getMinDate = () => {
    const today = new Date();
    today.setDate(today.getDate() + 7);
    return today;
  };

  const getMaxDate = () => {
    return new Date(searchedVoucher.expirationDate);
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
    <div className='min-h-screen px-6 mx-auto max-w-7xl xl:px-0'>
      <h1 className='mt-10 mb-3 text-4xl font-bold'>Utilizza il tuo Voucher</h1>
      <p className='mb-8 font-normal text-gray-500'>
        Utilizza il codice del tuo voucher per riscattare o prenotare la tua
        esperienza
      </p>

      <div className='max-w-3xl mx-auto'>
        <div className='flex items-center justify-center p-1 mb-6 bg-gray-100 rounded-lg'>
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
            className={`flex justify-center items-center text-sm font-medium rounded-sm grow p-1 cursor-pointer ${
              !redeemVoucherOption && 'bg-white'
            }`}
            onClick={() => {
              setRedeemVoucherOption(false);
            }}
          >
            {isLoading && (
              <span className='inline-block me-2'>
                <Spinner size='sm' />
              </span>
            )}
            I miei Voucher {!isLoading && `(${redeemedVouchers.length})`}
          </button>
        </div>

        <div className='p-5 mb-16 bg-white border rounded-lg shadow border-gray-400/10'>
          {redeemVoucherOption ? (
            // Riscatta Voucher
            <div>
              <div className='flex justify-start items-center gap-2 mb-1.5'>
                <Ticket className='text-primary pt-0.5' />
                <h4 className='text-2xl font-semibold'>
                  Riscatta il tuo Voucher
                </h4>
              </div>

              <p className='mb-6 text-sm text-gray-500'>
                Inserisci il codice del tuo voucher ricevuto via email o tramite
                cofanetto regalo
              </p>

              <p className='mb-2 text-sm font-bold text-gray-500'>
                Codice Voucher
              </p>
              <form
                className='items-center justify-center gap-2 mb-4 xs:flex'
                onSubmit={(e) => {
                  e.preventDefault();
                }}
              >
                <input
                  type='text'
                  className='text-xs xs:text-sm sm:text-base max-[480px]:w-full grow bg-background border border-gray-400/20 rounded-md px-3 py-2 max-[480px]:mb-4'
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

              {isSearchingVoucher && <SkeletonText />}

              {voucherNotFound && (
                <div className='px-4 py-3 mx-3 rounded-lg bg-red-200/60 '>
                  <p className='text-gray-600'>
                    Il codice inserito non corrisponde a nessun voucher o il
                    voucher cercato è già stato riscattato
                  </p>
                </div>
              )}

              {searchedVoucher !== null && (
                <div className='px-5 py-6 border rounded-lg border-gray-400/30'>
                  <h4 className='mb-2 text-xl'>{searchedVoucher.title}</h4>

                  <p className='mb-4 text-sm text-gray-600'>
                    È possibile riscuotere il Voucher entro il{' '}
                    {new Date(
                      searchedVoucher.expirationDate
                    ).toLocaleDateString('it-IT', {
                      year: 'numeric',
                      month: 'long',
                      day: 'numeric',
                    })}
                  </p>

                  <p className='mb-4 text-sm text-gray-500'>
                    Scegli una data per prenotare la tua esperienza
                  </p>

                  <form
                    onSubmit={(e) => {
                      sendReservationVoucherForm(e);
                    }}
                    className='w-full'
                  >
                    <label className='block mb-1 text-sm font-semibold'>
                      Data desiderata
                    </label>

                    <DatePicker
                      selected={reservationDate}
                      onChange={(date) => setReservationDate(date)}
                      minDate={getMinDate()}
                      maxDate={getMaxDate()}
                      filterDate={isWeekday}
                      placeholderText='Seleziona una data (solo lun-ven)'
                      dateFormat='dd/MM/yyyy'
                      className='w-full px-4 py-2 mb-2 border rounded-lg bg-background border-gray-400/30'
                      wrapperClassName='w-full'
                    />

                    <p className='mb-5 text-sm text-gray-500'>
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
          ) : // I miei Voucher

          isLoading ? (
            <SkeletonList />
          ) : (
            <div>
              <div className='flex justify-start items-center gap-2 mb-1.5'>
                <TicketCheck className='text-primary pt-0.5' />
                <h4 className='text-2xl font-semibold'>I miei Voucher</h4>
              </div>

              <p className='mb-6 text-sm text-gray-500'>
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
                      <div className='flex items-center justify-between gap-4 mb-2'>
                        <h4 className='text-xl'>{voucher.title}</h4>
                        <span
                          className={`max-[480px]:hidden text-xs  rounded-md px-2 py-1 ${
                            checkIsPassed(voucher.reservationDate)
                              ? 'bg-gray-300'
                              : 'bg-green-200 text-gray-600'
                          }`}
                        >
                          Prenotato
                        </span>
                      </div>

                      <p className='mb-3 text-xs'>{voucher.voucherCode}</p>

                      <p className='mb-3 text-sm text-gray-500'>
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
                  <p className='mb-2 font-medium text-center text-gray-500'>
                    Non hai ancora nessun voucher prenotato
                  </p>
                  <p className='text-sm font-medium text-center text-gray-500'>
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
                      <CircleAlert className='mx-auto mb-4 text-red-500 h-14 w-14' />
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

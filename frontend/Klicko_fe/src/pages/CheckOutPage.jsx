import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import Button from '../components/ui/Button';
import { useNavigate } from 'react-router-dom';
import { setUserCart } from '../redux/actions';
import { toast } from 'sonner';
import { BadgePercent, X } from 'lucide-react';
import StripeContainer from '../components/StripeContainer';
import AddressFormComponent from '../components/AddressFormComponent';

const CheckOutPage = () => {
  const backendUrl = import.meta.env.VITE_BACKEND_URL;

  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [couponCode, setCouponCode] = useState('');
  const [selectedCoupon, setSelectedCoupon] = useState(null);
  const [couponError, setCouponError] = useState('');

  const [name, setName] = useState('');
  const [surname, setSurname] = useState('');
  const [city, setCity] = useState('');
  const [cap, setCap] = useState('');
  const [address, setAddress] = useState('');
  const [showError, setShowError] = useState(false);

  const cart = useSelector((state) => {
    return state.cart;
  });

  const profile = useSelector((state) => {
    return state.profile;
  });

  const fidelityCard = useSelector((state) => {
    return state.fidelityCard;
  });

  const shippingPrice = fidelityCard?.points >= 1000 ? 0 : 4.99;

  const navigate = useNavigate();

  const dispatch = useDispatch();

  const checkAuthentication = () => {
    let tokenObj = localStorage.getItem('klicko_token');

    if (!tokenObj) {
      navigate('/login');
    } else {
      setIsAuthenticated(true);
    }
  };

  const getUserCart = async () => {
    try {
      const response = await fetch(
        `${backendUrl}/Cart/GetCart/${profile.cartId}`,
        {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
          },
        }
      );
      if (response.ok) {
        const data = await response.json();

        dispatch(setUserCart(data.cart));
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch (e) {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>{e.message}</p>
        </>
      );
      navigate('/cart');
    }
  };

  const getSubTotalPrice = () => {
    return cart.experiences.reduce(
      (sum, element) =>
        sum + element.price * (1 - element.sale / 100) * element.quantity,
      0
    );
  };

  const getFinalPrice = () => {
    return selectedCoupon !== null
      ? getSubTotalPrice() * (1 - selectedCoupon.percentualSaleAmount / 100) -
          selectedCoupon.fixedSaleAmount +
          shippingPrice
      : getSubTotalPrice() + shippingPrice;
  };

  const getCouponSale = () => {
    return (
      getSubTotalPrice() * (selectedCoupon.percentualSaleAmount / 100) +
      selectedCoupon.fixedSaleAmount
    );
  };

  const getCoupon = async () => {
    try {
      let tokenObj = localStorage.getItem('klicko_token');

      if (!tokenObj) {
        navigate('/login');
      }

      let token = JSON.parse(tokenObj).token;

      const response = await fetch(
        `${backendUrl}/Coupon/getCouponByCode/${couponCode.toUpperCase()}`,
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

        if (data.coupon.minimumAmount <= getSubTotalPrice()) {
          setSelectedCoupon(data.coupon);
          setCouponCode('');
        } else {
          setCouponError(
            `È richiesto un importo minimo di ${data.coupon.minimumAmount
              .toFixed(2)
              .replace('.', ',')} € per utilizzare questo codice`
          );
          setCouponCode('');
          setSelectedCoupon(null);
        }
      } else {
        throw new Error();
      }
    } catch {
      setCouponError(`Coupon non valido`);
    }
  };

  const checkFormFields = () => {
    if (
      name.trim() === '' ||
      surname.trim() === '' ||
      city.trim() === '' ||
      cap.trim() === '' ||
      address.trim() === ''
    ) {
      setShowError(true);
      return false;
    } else {
      return true;
    }
  };

  const sendOrder = async () => {
    try {
      let tokenObj = localStorage.getItem('klicko_token');

      if (!tokenObj) {
        navigate('/login');
      }

      let token = JSON.parse(tokenObj).token;

      const experiencesList = [];

      cart.experiences.forEach((exp) => {
        experiencesList.push({
          experienceId: exp.experienceId,
          quantity: exp.quantity,
        });
      });

      const body = {
        orderExperiences: experiencesList,
      };

      if (selectedCoupon !== null) {
        body.couponId = selectedCoupon.couponId;
      }

      const response = await fetch(`${backendUrl}/Order`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(body),
      });
      if (response.ok) {
        const data = await response.json();

        toast.success(
          <>
            <p className='font-bold'>Acquisto confermato!</p>
            <p>Acquisto effettuato con successo!</p>
          </>
        );
        navigate(`/orderConfirmation/${data.orderId}`);
      } else {
        throw new Error(`Errore nella conferma dell'ordine!`);
      }
    } catch (e) {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>{e.message}</p>
        </>
      );
    }
  };

  useEffect(() => {
    checkAuthentication();

    if (isAuthenticated) {
      getUserCart();
    }
  }, [isAuthenticated]);

  return (
    <div className='min-h-screen px-6 mx-auto mt-8 max-w-7xl xl:px-0'>
      <h1 className='mb-6 text-3xl font-bold'>Completa il tuo ordine</h1>
      {cart?.cartId && (
        <div className='grid items-start justify-between grid-cols-3 gap-6'>
          {/* riepilogo ordine */}
          {cart.experiences.length === 0 && navigate('/cart')}
          <div className='col-span-3 px-6 py-5 bg-white rounded-lg shadow lg:col-span-1'>
            <h2 className='mb-2 text-2xl font-bold'>Riepilogo ordine</h2>
            <p className='mb-5 text-gray-500'>
              {cart.experiences.length} esperienze
            </p>

            <div className='mb-5'>
              {cart.experiences.length > 0 &&
                cart.experiences.map((exp, index) => (
                  <div
                    key={index}
                    className='grid items-center grid-cols-12 gap-2 py-3 border-b border-gray-400/40'
                  >
                    <div className='col-span-1 font-semibold text-end'>
                      {exp.quantity}x
                    </div>
                    <div className='col-span-7 xs:col-span-8 sm:text-sm ps-1'>
                      {exp.title}
                    </div>
                    <div className='col-span-4 font-semibold xs:col-span-3 text-end'>
                      {(exp.price * (1 - exp.sale / 100))
                        .toFixed(2)
                        .replace('.', ',')}{' '}
                      €
                    </div>
                  </div>
                ))}
            </div>

            <div className='mb-8'>
              <h5 className='mb-2 text-sm'>Hai un codice sconto?</h5>
              <form
                className='flex items-center justify-center gap-4'
                onSubmit={(e) => {
                  e.preventDefault();
                  getCoupon();
                }}
              >
                <input
                  type='text'
                  className='grow max-w-3/4 bg-background border border-gray-400/30 rounded-lg px-3 py-1.5 text-sm xs:text-base'
                  placeholder='Inserisci il codice'
                  value={couponCode}
                  onChange={(e) => {
                    setCouponCode(e.target.value);
                  }}
                />
                <Button type='submit' variant='outline'>
                  <BadgePercent className='w-5 h-5' />
                </Button>
              </form>

              {couponError.length > 0 && (
                <p className='relative py-3 mx-4 mt-3 text-sm rounded-lg ps-4 pe-6 bg-red-200/50'>
                  {couponError}
                  <X
                    className='absolute w-4 h-4 cursor-pointer top-2 end-2'
                    onClick={() => {
                      setCouponError('');
                    }}
                  />
                </p>
              )}

              {selectedCoupon !== null && (
                <div className='relative bg-gray-100 rounded-lg px-4 py-2.5 font-medium mt-4 mx-3'>
                  <p className='flex items-center justify-start gap-2'>
                    {selectedCoupon.code}
                    <span className='text-sm text-gray-500'>
                      (-
                      {selectedCoupon.percentualSaleAmount > 0
                        ? `${selectedCoupon.percentualSaleAmount}%`
                        : selectedCoupon.fixedSaleAmount > 0 &&
                          `${selectedCoupon.fixedSaleAmount} €`}
                      )
                    </span>
                  </p>
                  <X
                    className='absolute w-4 h-4 cursor-pointer top-2 end-2'
                    onClick={() => {
                      setSelectedCoupon(null);
                    }}
                  />
                </div>
              )}
            </div>

            <div className='flex items-center justify-between mb-2'>
              <span className='font-medium text-gray-500'>Subtotale:</span>
              <span className='font-semibold text-gray-500'>
                {getSubTotalPrice().toFixed(2).replace('.', ',')} €
              </span>
            </div>

            {selectedCoupon !== null && (
              <div className='flex items-center justify-between mb-2'>
                <span className='font-medium text-gray-500'>
                  Sconto coupon:
                </span>
                <span className='font-semibold text-gray-500'>
                  -{getCouponSale().toFixed(2).replace('.', ',')} €
                </span>
              </div>
            )}

            <div className='flex items-center justify-between mb-2'>
              <span className='font-medium text-gray-500'>Spedizione:</span>
              <span className='font-semibold text-gray-500'>
                {shippingPrice.toFixed(2).replace('.', ',')} €
              </span>
            </div>

            <div className='flex items-center justify-between'>
              <span className='text-xl font-bold'>Totale</span>
              <span className='text-lg font-bold'>
                {getFinalPrice().toFixed(2).replace('.', ',')} €
              </span>
            </div>
          </div>

          {/* modalità pagamento */}
          <div className='col-span-3 lg:col-span-2'>
            <div className='px-6 py-5 mb-10 bg-white rounded-lg shadow'>
              <h2 className='mb-2 text-2xl font-bold'>Indirizzo spedizione</h2>
              <AddressFormComponent
                name={name}
                setName={(data) => setName(data)}
                surname={surname}
                setSurname={(data) => setSurname(data)}
                city={city}
                setCity={(data) => setCity(data)}
                cap={cap}
                setCap={(data) => setCap(data)}
                address={address}
                setAddress={(data) => setAddress(data)}
                showError={showError}
              />
            </div>

            <div className='px-6 py-5 mb-10 bg-white rounded-lg shadow'>
              <h2 className='mb-2 text-2xl font-bold'>Pagamento</h2>
              {/* Stripe Container */}
              <StripeContainer
                sendOrder={sendOrder}
                orderAmount={getFinalPrice()}
                checkFormFields={checkFormFields}
              />
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default CheckOutPage;

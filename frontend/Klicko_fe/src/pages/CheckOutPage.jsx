import React, { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import Button from '../components/ui/Button';
import { useNavigate } from 'react-router-dom';
import { emptyCart } from '../redux/actions';
import { toast } from 'sonner';
import { BadgePercent, X } from 'lucide-react';
import StripeContainer from '../components/StripeContainer';

const CheckOutPage = () => {
  const [couponCode, setCouponCode] = useState('');
  const [selectedCoupon, setSelectedCoupon] = useState(null);
  const [couponError, setCouponError] = useState('');

  const cart = useSelector((state) => {
    return state.cart;
  });

  const fidelityCard = useSelector((state) => {
    return state.fidelityCard;
  });

  const shippingPrice = fidelityCard?.points >= 1000 ? 0 : 4.99;

  const navigate = useNavigate();

  const dispatch = useDispatch();

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
        `https://localhost:7235/api/Coupon/getCouponByCode/${couponCode.toUpperCase()}`,
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

        console.log(data);

        if (data.coupon.minimumAmount <= getSubTotalPrice()) {
          setSelectedCoupon(data.coupon);
          setCouponCode('');
          // toast.success('Acquisto effettuato con successo!');
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

      const response = await fetch(`https://localhost:7235/api/Order`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(body),
      });
      if (response.ok) {
        const data = await response.json();

        dispatch(emptyCart());
        console.log(data);
        toast.success('Acquisto effettuato con successo!');
        navigate(`/orderConfirmation/${data.orderId}`);
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch {
      console.log('Error');
    }
  };

  return (
    <div className='max-w-7xl mx-auto min-h-screen mt-8 px-6 xl:px-0'>
      <h1 className='text-3xl font-bold mb-6'>Completa il tuo ordine</h1>
      {cart?.cartId && (
        <div className='grid grid-cols-3 justify-between items-start gap-6'>
          {/* riepilogo ordine */}
          <div className='col-span-3 lg:col-span-1 bg-white rounded-lg px-6 py-5 shadow'>
            <h2 className='text-2xl font-bold mb-2'>Riepilogo ordine</h2>
            <p className='text-gray-500 mb-5'>
              {cart.experiences.length} esperienze
            </p>

            <div className='mb-5'>
              {cart.experiences.length > 0 &&
                cart.experiences.map((exp, index) => (
                  <div
                    key={index}
                    className='grid grid-cols-12 items-center gap-2 border-b border-gray-400/40 py-3'
                  >
                    <div className='col-span-1 font-semibold text-end'>
                      {exp.quantity}x
                    </div>
                    <div className='col-span-8 text-sm'>{exp.title}</div>
                    <div className='col-span-3 text-end font-semibold'>
                      {(exp.price * (1 - exp.sale / 100))
                        .toFixed(2)
                        .replace('.', ',')}{' '}
                      €
                    </div>
                  </div>
                ))}
            </div>

            <div className='mb-8'>
              <h5 className='text-sm mb-2'>Hai un codice sconto?</h5>
              <form
                className='flex justify-center items-center gap-4'
                onSubmit={(e) => {
                  e.preventDefault();
                  getCoupon();
                }}
              >
                <input
                  type='text'
                  className='grow bg-background border border-gray-400/30 rounded-lg px-3 py-1.5'
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
                <p className='relative mx-4 mt-3 ps-4 pe-6 py-3 text-sm bg-red-200/50 rounded-lg'>
                  {couponError}
                  <X
                    className='absolute top-2 end-2 w-4 h-4 cursor-pointer'
                    onClick={() => {
                      setCouponError('');
                    }}
                  />
                </p>
              )}

              {selectedCoupon !== null && (
                <div className='relative bg-gray-100 rounded-lg px-4 py-2.5 font-medium mt-4 mx-3'>
                  <p className='flex justify-start items-center gap-2'>
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
                    className='absolute top-2 end-2 w-4 h-4 cursor-pointer'
                    onClick={() => {
                      setSelectedCoupon(null);
                    }}
                  />
                </div>
              )}
            </div>

            <div className='flex justify-between items-center mb-2'>
              <span className='text-gray-500 font-medium'>Subtotale:</span>
              <span className='text-gray-500 font-semibold'>
                {getSubTotalPrice().toFixed(2).replace('.', ',')} €
              </span>
            </div>

            {selectedCoupon !== null && (
              <div className='flex justify-between items-center mb-2'>
                <span className='text-gray-500 font-medium'>
                  Sconto coupon:
                </span>
                <span className='text-gray-500 font-semibold'>
                  -{getCouponSale().toFixed(2).replace('.', ',')} €
                </span>
              </div>
            )}

            <div className='flex justify-between items-center mb-2'>
              <span className='text-gray-500 font-medium'>Spedizione:</span>
              <span className='text-gray-500 font-semibold'>
                {shippingPrice.toFixed(2).replace('.', ',')} €
              </span>
            </div>

            <div className='flex justify-between items-center'>
              <span className='text-xl font-bold'>Totale</span>
              <span className='text-lg font-bold'>
                {getFinalPrice().toFixed(2).replace('.', ',')} €
              </span>
            </div>
          </div>

          {/* modalità pagamento */}
          <div className='col-span-3 lg:col-span-2 bg-white rounded-lg px-6 py-5 shadow'>
            <h2 className='text-2xl font-bold mb-2'>Pagamento</h2>
            <StripeContainer
              sendOrder={sendOrder}
              orderAmount={getFinalPrice()}
            />
          </div>

          {/* Stripe Container */}
        </div>
      )}
    </div>
  );
};

export default CheckOutPage;

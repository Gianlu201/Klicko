import { CircleCheck, Clock, Package, Ticket, Truck } from 'lucide-react';
import React, { useEffect, useState } from 'react';
import Button from '../components/ui/Button';
import { useNavigate, useParams } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { emptyCart } from '../redux/actions';
import { toast } from 'sonner';

const OrderConfirmationPage = () => {
  const [order, setOrder] = useState(null);

  const params = useParams();

  const navigate = useNavigate();

  const dispatch = useDispatch();

  const getOrder = async (orderId) => {
    try {
      let tokenObj = localStorage.getItem('klicko_token');

      if (!tokenObj) {
        navigate('/login');
      }

      let token = JSON.parse(tokenObj).token;

      const response = await fetch(
        `https://localhost:7235/api/Order/getOrderById/${orderId}`,
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

        setOrder(data.order);
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
      navigate('/');
    }
  };

  useEffect(() => {
    getOrder(params.orderId);
    dispatch(emptyCart());
  }, []);

  return (
    <div className='px-6 xl-px-0'>
      {order !== null && (
        <div className='max-w-3xl mx-auto bg-white border border-gray-400/40 shadow rounded-2xl my-12 px-8 py-8'>
          <CircleCheck className='w-14 h-14 mx-auto text-green-500 mb-4' />
          <h1 className='text-4xl font-bold tracking-tight text-center mb-8'>
            Ordine Confermato!
          </h1>

          <div className='text-center py-4 bg-gray-100 rounded-2xl mb-8'>
            <p className='text-lg text-gray-500 font-semibold mb-2'>
              Numero ordine
            </p>
            <p className='text-lg font-bold'>order-{order.orderNumber}</p>
          </div>

          <p className='text-lg text-gray-500 font-medium text-center mb-8'>
            Grazie per il tuo acquisto! Riceverai presto una email di conferma
            con i dettagli del tuo ordine.
          </p>

          <div className='border border-gray-400/30 rounded-lg px-5 py-4 mb-12'>
            <h3 className='flex justify-start items-center gap-2 text-primary text-xl font-semibold mb-4'>
              <Ticket />I tuoi Voucher
            </h3>

            <p className='text-gray-500 mb-3'>
              Ti abbiamo inviato i seguenti codici voucher che riceverai anche
              in formato fisico a casa tua:
            </p>

            {order.vouchers.map((voucher) => (
              <div
                key={voucher.voucherId}
                className='bg-gray-100 rounded-lg px-4 py-3 mb-4'
              >
                <p className='text-lg font-medium mb-1'>{voucher.title}</p>
                <p className='text-sm italic'>{voucher.voucherCode}</p>
              </div>
            ))}
          </div>

          <div className='grid grid-cols-4 items-start gap-6 mb-8'>
            <div className='text-primary'>
              <Clock className='w-6 h-6 md:w-10 md:h-10 mx-auto mb-2' />
              <p className='text-sm md:text-lg font-medium text-center'>
                Ordine confermato
              </p>
            </div>

            <div className='text-gray-500'>
              <Package className='w-6 h-6 md:w-10 md:h-10 mx-auto mb-2' />
              <p className='text-sm md:text-lg font-medium text-center'>
                In preparazione
              </p>
            </div>

            <div className='text-gray-500'>
              <Truck className='w-6 h-6 md:w-10 md:h-10 mx-auto mb-2' />
              <p className='text-sm md:text-lg font-medium text-center'>
                Spedito
              </p>
            </div>

            <div className='text-gray-500'>
              <CircleCheck className='w-6 h-6 md:w-10 md:h-10 mx-auto mb-2' />
              <p className='text-sm md:text-lg font-medium text-center'>
                Consegnato
              </p>
            </div>
          </div>

          <div className='flex justify-center items-center gap-6'>
            <Button
              variant='outline'
              onClick={() => {
                navigate('/');
              }}
            >
              Torna alla home
            </Button>
            <Button
              variant='primary'
              onClick={() => {
                navigate('/dashboard/orders');
              }}
            >
              I miei ordini
            </Button>
          </div>
        </div>
      )}
    </div>
  );
};

export default OrderConfirmationPage;

import { CircleCheck, Clock, Package, Ticket, Truck } from 'lucide-react';
import React, { useEffect, useState } from 'react';
import Button from '../components/ui/Button';
import { useNavigate, useParams } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { emptyCart } from '../redux/actions';
import { toast } from 'sonner';

const OrderConfirmationPage = () => {
  const backendUrl = import.meta.env.VITE_BACKEND_URL;

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
        `${backendUrl}/Order/getOrderById/${orderId}`,
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
        <div className='max-w-3xl px-8 py-8 mx-auto my-12 bg-white border shadow border-gray-400/40 rounded-2xl'>
          <CircleCheck className='mx-auto mb-4 text-green-500 w-14 h-14' />
          <h1 className='mb-8 text-4xl font-bold tracking-tight text-center'>
            Ordine Confermato!
          </h1>

          <div className='py-4 mb-8 text-center bg-gray-100 rounded-2xl'>
            <p className='mb-2 text-lg font-semibold text-gray-500'>
              Numero ordine
            </p>
            <p className='text-lg font-bold'>order-{order.orderNumber}</p>
          </div>

          <p className='mb-8 text-lg font-medium text-center text-gray-500'>
            Grazie per il tuo acquisto! Riceverai presto una email di conferma
            con i dettagli del tuo ordine.
          </p>

          <div className='px-5 py-4 mb-12 border rounded-lg border-gray-400/30'>
            <h3 className='flex items-center justify-start gap-2 mb-4 text-xl font-semibold text-primary'>
              <Ticket />I tuoi Voucher
            </h3>

            <p className='mb-3 text-gray-500'>
              Ti abbiamo inviato i seguenti codici voucher che riceverai anche
              in formato fisico a casa tua:
            </p>

            {order.vouchers.map((voucher) => (
              <div
                key={voucher.voucherId}
                className='px-4 py-3 mb-4 bg-gray-100 rounded-lg'
              >
                <p className='mb-1 text-lg font-medium'>{voucher.title}</p>
                <p className='text-sm italic'>{voucher.voucherCode}</p>
              </div>
            ))}
          </div>

          <div className='grid items-start grid-cols-2 gap-6 mb-8 sm:grid-cols-4'>
            <div className='text-primary'>
              <Clock className='w-6 h-6 mx-auto mb-2 md:w-10 md:h-10' />
              <p className='text-sm font-medium text-center md:text-lg'>
                Ordine confermato
              </p>
            </div>

            <div className='text-gray-500'>
              <Package className='w-6 h-6 mx-auto mb-2 md:w-10 md:h-10' />
              <p className='text-sm font-medium text-center md:text-lg'>
                In preparazione
              </p>
            </div>

            <div className='text-gray-500'>
              <Truck className='w-6 h-6 mx-auto mb-2 md:w-10 md:h-10' />
              <p className='text-sm font-medium text-center md:text-lg'>
                Spedito
              </p>
            </div>

            <div className='text-gray-500'>
              <CircleCheck className='w-6 h-6 mx-auto mb-2 md:w-10 md:h-10' />
              <p className='text-sm font-medium text-center md:text-lg'>
                Consegnato
              </p>
            </div>
          </div>

          <div className='flex items-center justify-center gap-6 max-xs:flex-col'>
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

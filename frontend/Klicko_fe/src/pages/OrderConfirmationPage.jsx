import { CircleCheck, Clock, Package } from 'lucide-react';
import React, { useEffect, useState } from 'react';
import Button from '../components/ui/Button';
import { useNavigate, useParams } from 'react-router-dom';

const OrderConfirmationPage = () => {
  const [order, setOrder] = useState(null);

  const params = useParams();

  const navigate = useNavigate();

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

        console.log(data);

        setOrder(data.order);

        // navigate(`/orderConfirmation/${data.orderId}`);
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch {
      console.log('Error');
    }
  };

  useEffect(() => {
    getOrder(params.orderId);
  }, []);

  return (
    <>
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

          <div className='grid grid-cols-3 items-center mb-8'>
            <div className='text-primary'>
              <Clock className='w-10 h-10 mx-auto' />
              <p className='text-lg font-medium text-center'>
                Ordine confermato
              </p>
            </div>

            <div className='text-gray-500'>
              <Package className='w-10 h-10 mx-auto' />
              <p className='text-lg font-medium text-center'>In preparazione</p>
            </div>

            <div className='text-gray-500'>
              <CircleCheck className='w-10 h-10 mx-auto' />
              <p className='text-lg font-medium text-center'>Consegnato</p>
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
    </>
  );
};

export default OrderConfirmationPage;

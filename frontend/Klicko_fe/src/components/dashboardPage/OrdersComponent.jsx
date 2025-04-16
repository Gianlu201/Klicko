import { Calendar, Package, ShoppingBag, TicketX } from 'lucide-react';
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

const OrdersComponent = () => {
  const [orders, setOrders] = useState([]);

  const navigate = useNavigate();

  const options = [
    {
      id: 1,
      title: 'Totale ordini',
      value: 0,
      icon: <ShoppingBag className='text-primary/40 w-8 h-8' />,
    },
    {
      id: 2,
      title: 'In attesa',
      value: 0,
      icon: <Calendar className='text-amber-500/40 w-8 h-8' />,
    },
    {
      id: 3,
      title: 'Completati',
      value: 0,
      icon: <Package className='text-green-500/40 w-8 h-8' />,
    },
    {
      id: 4,
      title: 'Cancellati',
      value: 0,
      icon: <TicketX className='text-red-500/40 w-8 h-8' />,
    },
  ];

  const getAllUserOrders = async () => {
    try {
      let tokenObj = localStorage.getItem('klicko_token');

      if (!tokenObj) {
        navigate('/login');
      }

      let token = JSON.parse(tokenObj).token;

      const response = await fetch(
        `https://localhost:7235/api/Order/getAllUserOrders`,
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

        setOrders(data.orders);
        // dispatch(emptyCart());
        // navigate('/');
        // toast.success('Acquisto effettuato con successo!');

        // dispatch(cartModified());
        // console.log(data.cart);

        console.log(data);
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch {
      console.log('Error');
    }
  };

  useEffect(() => {
    getAllUserOrders();
  }, []);

  return (
    <>
      <h2 className='text-2xl font-bold mb-2'>I tuoi ordini</h2>
      <p className='text-gray-500 font-normal mb-6'>
        Visualizza e gestisci i tuoi acquisti
      </p>

      {/* orders overview */}
      <div className='grid grid-cols-4 gap-6 mb-6'>
        {options.map((opt) => (
          <div
            key={opt.id}
            className='flex justify-between items-start border border-gray-400/40 shadow-xs rounded-xl px-4 py-6'
          >
            <div className='flex flex-col justify-center items-start gap-2'>
              <span className='text-gray-500 font-medium text-sm'>
                {opt.title}
              </span>
              <span className='text-2xl font-semibold'>{opt.value}</span>
            </div>
            <div>{opt.icon}</div>
          </div>
        ))}
      </div>

      {/* tabella storico ordini */}
      <div className='border border-gray-400/40 shadow-sm rounded-xl px-6 py-5'>
        {orders.length > 0 ? (
          <>
            <h2 className='text-xl font-semibold mb-2'>Tutti gli ordini</h2>
            <p className='text-gray-500 font-normal mb-6'>
              {orders.length} trovati
            </p>

            <div>
              {orders.map((order) => (
                <div
                  key={order.orderNumber}
                  className='flex justify-between items-center'
                >
                  <div className='grow flex gap-2'>
                    <span>Ordine</span>
                    <h6>#{order.orderNumber}</h6>
                  </div>
                  <div className='flex justify-between items-center gap-4'>
                    <span>
                      {new Date(order.createdAt).toLocaleDateString('it-IT', {
                        year: 'numeric',
                        month: 'long',
                        day: 'numeric',
                      })}
                    </span>
                    <span>{order.state}</span>
                  </div>
                </div>
              ))}
            </div>
          </>
        ) : (
          <div className='flex flex-col justify-center items-center gap-2 py-10'>
            <h3 className='text-xl font-semibold'>Nessun ordine trovato</h3>
            <p className='text-gray-500 font-normal'>
              Non hai ancora effettuato nessun ordine.
            </p>
          </div>
        )}
      </div>
    </>
  );
};

export default OrdersComponent;

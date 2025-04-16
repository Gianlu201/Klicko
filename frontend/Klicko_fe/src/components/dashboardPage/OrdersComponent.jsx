import { Calendar, Package, ShoppingBag, TicketX } from 'lucide-react';
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Accordion from '../ui/Accordion';

const OrdersComponent = () => {
  const [orders, setOrders] = useState([]);
  const [filteredOrders, setFilteredOrders] = useState([]);

  const navigate = useNavigate();

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
        setFilteredOrders(data.orders);
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

  const getNumberOfPending = () => {
    let count = 0;

    const pendingOrders = orders.filter((order) => order.state === 'In attesa');

    if (pendingOrders !== null && pendingOrders.length !== 0) {
      count = pendingOrders.length;
    }

    return count;
  };

  const getNumberOfCompleted = () => {
    let count = 0;

    const completedOrders = orders.filter(
      (order) => order.state === 'Completato'
    );

    if (completedOrders !== null && completedOrders.length !== 0) {
      count = completedOrders.length;
    }

    return count;
  };

  const getNumberOfDeleted = () => {
    let count = 0;

    const deleteddOrders = orders.filter(
      (order) => order.state === 'Cancellato'
    );

    if (deleteddOrders !== null && deleteddOrders.length !== 0) {
      count = deleteddOrders.length;
    }

    return count;
  };

  const getStateStyle = (state) => {
    switch (state) {
      case 'In attesa':
        return 'bg-amber-200/60 border border-amber-500/40';

      case 'Completato':
        return 'bg-green-200/60 border border-green-500/40';

      case 'Cancellato':
        return 'bg-red-200/60 border border-red-500/40';

      default:
        return '';
    }
  };

  const filterBy = (filterState) => {
    if (filterState === 'getAll') {
      setFilteredOrders(orders);
    } else {
      const orderList = orders.filter((order) => order.state === filterState);

      setFilteredOrders(orderList);
    }
  };

  const options = [
    {
      id: 1,
      title: 'Totale ordini',
      value: orders.length,
      icon: <ShoppingBag className='text-primary/40 w-8 h-8' />,
      filter: 'getAll',
    },
    {
      id: 2,
      title: 'In attesa',
      value: getNumberOfPending(),
      icon: <Calendar className='text-amber-500/40 w-8 h-8' />,
      filter: 'In attesa',
    },
    {
      id: 3,
      title: 'Completati',
      value: getNumberOfCompleted(),
      icon: <Package className='text-green-500/40 w-8 h-8' />,
      filter: 'Completato',
    },
    {
      id: 4,
      title: 'Cancellati',
      value: getNumberOfDeleted(),
      icon: <TicketX className='text-red-500/40 w-8 h-8' />,
      filter: 'Cancellato',
    },
  ];

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
            className='flex justify-between items-start border border-gray-400/40 shadow-xs rounded-xl px-4 py-6 cursor-pointer hover:bg-background/80'
            onClick={() => {
              filterBy(opt.filter);
            }}
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
        {filteredOrders.length > 0 ? (
          <>
            <h2 className='text-xl font-semibold mb-2'>Tutti gli ordini</h2>
            <p className='text-gray-500 font-normal mb-6'>
              {filteredOrders.length} trovati
            </p>

            <div>
              {filteredOrders.map((order) => (
                <Accordion
                  intestation={
                    <div className='flex justify-between items-center'>
                      <div className='grow flex gap-2'>
                        <span>Ordine</span>
                        <h6>#{order.orderNumber}</h6>
                      </div>
                      <div className='flex justify-between items-center gap-4'>
                        <span className='text-sm'>
                          {new Date(order.createdAt).toLocaleDateString(
                            'it-IT',
                            {
                              year: 'numeric',
                              month: 'long',
                              day: 'numeric',
                            }
                          )}
                        </span>
                        <span
                          className={`text-xs rounded-full px-3 py-0.5 ${getStateStyle(
                            order.state
                          )}`}
                        >
                          {order.state}
                        </span>
                      </div>
                    </div>
                  }
                >
                  <table className='w-full mb-10'>
                    <thead>
                      <tr className='grid grid-cols-12 text-gray-500 text-sm font-normal border-b border-gray-400/40 p-3 hover:bg-gray-100'>
                        <th className='col-span-7 text-start'>Esperienza</th>
                        <th className='col-span-1 text-center'>Quantità</th>
                        <th className='col-span-2 text-end'>Prezzo</th>
                        <th className='col-span-2 text-end'>Totale</th>
                      </tr>
                    </thead>

                    <tbody>
                      {order.experiences.length > 0 &&
                        order.experiences.map((exp) => (
                          <tr className='grid grid-cols-12 text-sm border-b border-gray-400/40 p-3 hover:bg-gray-100'>
                            <td className='col-span-7 text-start'>
                              {exp.title}
                            </td>
                            <td className='col-span-1 text-center'>
                              {exp.quantity}
                            </td>
                            <td className='col-span-2 text-end'>
                              {exp.price.toFixed(2).replace('.', ',')} €
                            </td>
                            <td className='col-span-2 text-end'>
                              {(exp.price * exp.quantity)
                                .toFixed(2)
                                .replace('.', ',')}{' '}
                              €
                            </td>
                          </tr>
                        ))}
                    </tbody>

                    <tfoot>
                      <tr className='grid grid-cols-12 font-bold p-3 hover:bg-gray-100'>
                        <td className='col-span-10 text-end'>Totale ordine:</td>
                        <td className='col-span-2 text-end'>
                          {order.totalPrice.toFixed(2).replace('.', ',')} €
                        </td>
                      </tr>
                    </tfoot>
                  </table>
                </Accordion>
              ))}
            </div>
          </>
        ) : orders.length === 0 ? (
          <div className='flex flex-col justify-center items-center gap-2 py-10'>
            <h3 className='text-xl font-semibold'>Nessun ordine trovato</h3>
            <p className='text-gray-500 font-normal'>
              Non hai ancora effettuato nessun ordine.
            </p>
          </div>
        ) : (
          <div className='flex flex-col justify-center items-center gap-2 py-10'>
            <h3 className='text-xl font-semibold'>Nessun ordine trovato</h3>
            <p className='text-gray-500 font-normal'>
              Non hai nessun ordine con questo stato.
            </p>
          </div>
        )}
      </div>
    </>
  );
};

export default OrdersComponent;

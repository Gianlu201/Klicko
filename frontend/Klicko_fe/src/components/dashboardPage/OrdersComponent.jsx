import { Calendar, Package, ShoppingBag, TicketX } from 'lucide-react';
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Accordion from '../ui/Accordion';
import { toast } from 'sonner';
import SkeletonText from '../ui/SkeletonText';

const OrdersComponent = () => {
  const backendUrl = import.meta.env.VITE_BACKEND_URL;

  const [isLoading, setIsLoading] = useState(true);
  const [orders, setOrders] = useState([]);
  const [filteredOrders, setFilteredOrders] = useState([]);

  const navigate = useNavigate();

  const getAllUserOrders = async () => {
    try {
      setIsLoading(true);

      let tokenObj = localStorage.getItem('klicko_token');

      if (!tokenObj) {
        navigate('/login');
      }

      let token = JSON.parse(tokenObj).token;

      const response = await fetch(`${backendUrl}/Order/getAllUserOrders`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${token}`,
        },
      });
      if (response.ok) {
        const data = await response.json();

        setIsLoading(false);
        setOrders(data.orders);
        setFilteredOrders(data.orders);
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
      setIsLoading(false);
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

      case 'Spedito':
        return 'bg-primary/40 border border-primary/30';

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
      icon: <ShoppingBag className='w-8 h-8 max-xs:hidden text-primary/40' />,
      filter: 'getAll',
    },
    {
      id: 2,
      title: 'In attesa',
      value: getNumberOfPending(),
      icon: <Calendar className='w-8 h-8 max-xs:hidden text-amber-500/40' />,
      filter: 'In attesa',
    },
    {
      id: 3,
      title: 'Completati',
      value: getNumberOfCompleted(),
      icon: <Package className='w-8 h-8 max-xs:hidden text-green-500/40' />,
      filter: 'Completato',
    },
    {
      id: 4,
      title: 'Cancellati',
      value: getNumberOfDeleted(),
      icon: <TicketX className='w-8 h-8 max-xs:hidden text-red-500/40' />,
      filter: 'Cancellato',
    },
  ];

  useEffect(() => {
    getAllUserOrders();
  }, []);

  return (
    <>
      <h2 className='mb-2 text-2xl font-bold'>I tuoi ordini</h2>
      <p className='mb-6 font-normal text-gray-500'>
        Visualizza e gestisci i tuoi acquisti
      </p>

      {/* orders overview */}
      <div className='grid grid-cols-2 gap-6 mb-6 md:grid-cols-4'>
        {options.map((opt) => (
          <div
            key={opt.id}
            className='flex justify-between items-start border border-gray-400/40 shadow-xs rounded-xl px-2 min-[480px]:px-4 py-6 cursor-pointer hover:bg-background/80'
            onClick={() => {
              filterBy(opt.filter);
            }}
          >
            <div className='flex flex-col items-start justify-center gap-2'>
              <span className='text-sm font-medium text-gray-500'>
                {opt.title}
              </span>
              <span className='text-2xl font-semibold'>{opt.value}</span>
            </div>
            <div>{opt.icon}</div>
          </div>
        ))}
      </div>

      {/* tabella storico ordini */}
      <div className='px-6 py-5 border shadow-sm border-gray-400/40 rounded-xl'>
        {isLoading ? (
          <SkeletonText />
        ) : filteredOrders.length > 0 ? (
          <>
            <h2 className='mb-2 text-xl font-semibold'>Tutti gli ordini</h2>
            <p className='mb-6 font-normal text-gray-500'>
              {filteredOrders.length} ordini trovati
            </p>

            <div>
              {filteredOrders.map((order) => (
                <Accordion
                  key={order.orderId}
                  intestation={
                    <div className='flex items-center justify-between cursor-pointer'>
                      <div className='flex gap-2 grow'>
                        <span className='max-[480px]:hidden'>Ordine</span>
                        <h6 className='max-[480px]:text-xs'>
                          #{order.orderNumber}
                        </h6>
                      </div>
                      <div className='flex items-center justify-between gap-4'>
                        <span className='hidden text-sm md:block'>
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
                          className={`text-xs rounded-full px-1.5 min-[480px]:px-3 py-0.5 ${getStateStyle(
                            order.state
                          )}`}
                        >
                          {order.state}
                        </span>
                      </div>
                    </div>
                  }
                >
                  <div className='overflow-x-auto'>
                    <table className='min-w-[300px] w-full mb-10'>
                      <thead>
                        <tr className='grid grid-cols-12 p-3 text-sm font-normal text-gray-500 border-b border-gray-400/40 hover:bg-gray-100'>
                          <th className='col-span-7 text-start'>Esperienza</th>
                          <th className='col-span-2 text-center md:col-span-1'>
                            Quantità
                          </th>
                          <th className='hidden col-span-2 md:block text-end'>
                            Prezzo
                          </th>
                          <th className='col-span-3 md:col-span-2 text-end'>
                            Totale
                          </th>
                        </tr>
                      </thead>

                      <tbody>
                        {order.orderExperiences.length > 0 &&
                          order.orderExperiences.map((orderExp) => (
                            <tr
                              key={orderExp.orderExperienceId}
                              className='grid grid-cols-12 p-3 text-sm border-b border-gray-400/40 hover:bg-gray-100'
                            >
                              <td className='col-span-7 text-start'>
                                {orderExp.title}
                              </td>
                              <td className='col-span-2 text-center md:col-span-1'>
                                {orderExp.quantity}
                              </td>
                              <td className='hidden col-span-2 md:block text-end'>
                                {orderExp.unitPrice
                                  .toFixed(2)
                                  .replace('.', ',')}{' '}
                                €
                              </td>
                              <td className='col-span-3 md:col-span-2 text-end'>
                                {orderExp.totalPrice
                                  .toFixed(2)
                                  .replace('.', ',')}{' '}
                                €
                              </td>
                            </tr>
                          ))}
                      </tbody>

                      <tfoot>
                        {order.shippingPrice > 0 && (
                          <tr className='grid grid-cols-12 p-3 font-medium text-gray-500 hover:bg-gray-100'>
                            <td className='col-span-8 md:col-span-10 text-end'>
                              Spedizione:
                            </td>
                            <td className='col-span-4 md:col-span-2 text-end'>
                              {order.shippingPrice.toFixed(2).replace('.', ',')}{' '}
                              €
                            </td>
                          </tr>
                        )}

                        {order.totalDiscount > 0 && (
                          <tr className='grid grid-cols-12 p-3 font-medium text-gray-500 hover:bg-gray-100'>
                            <td className='col-span-8 md:col-span-10 text-end'>
                              Sconto coupon:
                            </td>
                            <td className='col-span-4 md:col-span-2 text-end'>
                              -
                              {order.totalDiscount.toFixed(2).replace('.', ',')}{' '}
                              €
                            </td>
                          </tr>
                        )}

                        <tr className='grid grid-cols-12 p-3 font-bold hover:bg-gray-100'>
                          <td className='col-span-8 md:col-span-10 text-end'>
                            Totale ordine:
                          </td>
                          <td className='col-span-4 md:col-span-2 text-end'>
                            {order.totalPrice.toFixed(2).replace('.', ',')} €
                          </td>
                        </tr>
                      </tfoot>
                    </table>
                  </div>
                </Accordion>
              ))}
            </div>
          </>
        ) : orders.length === 0 ? (
          <div className='flex flex-col items-center justify-center gap-2 py-10'>
            <h3 className='text-xl font-semibold'>Nessun ordine trovato</h3>
            <p className='font-normal text-gray-500'>
              Non hai ancora effettuato nessun ordine.
            </p>
          </div>
        ) : (
          <div className='flex flex-col items-center justify-center gap-2 py-10'>
            <h3 className='text-xl font-semibold'>Nessun ordine trovato</h3>
            <p className='font-normal text-gray-500'>
              Non hai nessun ordine con questo stato.
            </p>
          </div>
        )}
      </div>
    </>
  );
};

export default OrdersComponent;

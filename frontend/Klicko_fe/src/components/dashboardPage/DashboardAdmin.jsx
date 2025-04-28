import {
  BanknoteArrowUp,
  Calendar,
  Cog,
  Funnel,
  Package,
  Search,
  ShoppingBag,
  Users,
} from 'lucide-react';
import React, { useEffect, useState } from 'react';
import Button from '../ui/Button';
import { useNavigate } from 'react-router-dom';
import Accordion from '../ui/Accordion';
import BottomBanner from '../ui/BottomBanner';

const DashboardAdmin = () => {
  const [orders, setOrders] = useState([]);
  const [filteredOrders, setFilteredOrders] = useState([]);
  const [isBannerOpen, setIsBannerOpen] = useState(false);
  const [selectedOrder, setSelectedOrder] = useState(null);
  const [stateFilter, setStateFilter] = useState('');

  const navigate = useNavigate();

  const getAllOrders = async () => {
    try {
      let tokenObj = localStorage.getItem('klicko_token');

      if (!tokenObj) {
        navigate('/login');
      }

      let token = JSON.parse(tokenObj).token;

      const response = await fetch(`https://localhost:7235/api/Order`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${token}`,
        },
      });
      if (response.ok) {
        const data = await response.json();

        setOrders(data.orders);
        setFilteredOrders(data.orders);

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

  const getTotalTurnover = () => {
    let totalTurnover = 0;

    orders.forEach((order) => {
      if (order.state !== 'Cancellato') {
        totalTurnover += order.totalPrice;
      }
    });

    return totalTurnover;
  };

  const getCustomersNumber = () => {
    const customersEmailList = [];

    orders.forEach((order) => {
      if (
        order.user !== null &&
        !customersEmailList.includes(order.user.email)
      ) {
        customersEmailList.push(order.user.email);
      }
    });

    return customersEmailList.length;
  };

  const filterBy = (search, state) => {
    console.log(stateFilter);

    let orderList = [];

    if (state !== null && state !== undefined) {
      console.log('Sto per settare lo stato a ' + state);
      setStateFilter(state);

      if (state === '') {
        orderList = orders;
      } else {
        orderList = orders.filter((order) => order.state === state);
      }
    } else {
      if (stateFilter === '') {
        orderList = orders;
      } else {
        orderList = orders.filter((order) => order.state === stateFilter);
      }
    }

    if (search.trim !== '') {
      const filteredList = [];

      orderList.forEach((order) => {
        console.log(order.orderNumber.toString());
        if (
          order.orderNumber.toString().includes(search.toLowerCase()) ||
          order.totalPrice.toString().includes(search.toLowerCase()) ||
          order.user.firstName.toLowerCase().includes(search.toLowerCase()) ||
          order.user.lastName.toLowerCase().includes(search.toLowerCase()) ||
          order.user.email.toLowerCase().includes(search.toLowerCase())
        ) {
          filteredList.push(order);
        }
      });

      console.log(filteredList);

      return setFilteredOrders(filteredList);
    }

    console.log(orderList);

    return setFilteredOrders(orderList);
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

  const editOrderState = async (orderId, newState) => {
    try {
      let tokenObj = localStorage.getItem('klicko_token');

      if (!tokenObj) {
        navigate('/login');
      }

      let token = JSON.parse(tokenObj).token;

      const body = {
        state: newState,
      };

      const response = await fetch(
        `https://localhost:7235/api/Order/editOrderState/${orderId}`,
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
        const data = await response.json();
        console.log(data);

        getAllOrders();
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch {
      console.log('Error');
    }
  };

  const options = [
    {
      id: 1,
      title: 'Totale ordini',
      value: orders.length,
      icon: <ShoppingBag className='text-primary/40 w-8 h-8' />,
    },
    {
      id: 2,
      title: 'Fatturato totale',
      value: `${getTotalTurnover().toFixed(2).replace('.', ',')} €`,
      icon: <BanknoteArrowUp className='text-green-500/40 w-8 h-8' />,
    },
    {
      id: 3,
      title: 'Clienti',
      value: getCustomersNumber(),
      icon: <Users className='text-indigo-500/40 w-8 h-8' />,
    },
    {
      id: 4,
      title: 'In attesa',
      value: getNumberOfPending(),
      icon: <Calendar className='text-amber-500/40 w-8 h-8' />,
    },
  ];

  const stateOption = [
    {
      id: 1,
      option: 'In attesa',
    },
    {
      id: 2,
      option: 'Spedito',
    },
    {
      id: 3,
      option: 'Completato',
    },
    {
      id: 4,
      option: 'Cancellato',
    },
  ];

  useEffect(() => {
    getAllOrders();
  }, []);

  return (
    <>
      <h2 className='text-2xl font-bold mb-2'>Dashboard amministrativa</h2>
      <p className='text-gray-500 font-normal mb-6'>
        Panoramica di tutti gli ordini e le statistiche
      </p>

      {/* options overview */}
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

      <div className='flex justify-between items-center gap-4 mb-8'>
        <div className='relative grow'>
          <input
            type='text'
            placeholder='Cerca ordini, clienti...'
            className='bg-background border border-gray-400/30 rounded-xl py-2 ps-10 w-full'
            onChange={(e) => {
              filterBy(e.target.value);
            }}
          />
          <Search className='absolute top-1/2 left-3 -translate-y-1/2 w-5 h-5 pb-0.5' />
        </div>

        <select
          className='rounded-md border border-gray-300 bg-white py-2 px-4 pr-8 shadow-sm focus:border-primary focus:ring-primary focus:outline-none focus:ring-1 text-gray-700 text-sm'
          value={stateFilter}
          onChange={(e) => {
            filterBy('', e.target.value);
          }}
        >
          <option value=''>Tutti gli stati</option>
          {stateOption.map((state) => (
            <option key={state.id} value={state.option}>
              {state.option}
            </option>
          ))}
        </select>
      </div>

      {/* tabella storico ordini */}
      <div className='px-6 py-5 border border-gray-400/40 rounded-xl'>
        <h2 className='text-xl font-semibold mb-2'>Tutti gli ordini</h2>
        <p className='text-gray-500 font-normal mb-6'>
          {filteredOrders.length} ordini trovati
        </p>

        {filteredOrders.length > 0 ? (
          <div>
            <table className='w-full'>
              <thead>
                <tr className='grid grid-cols-12 text-gray-500 text-sm font-normal border-b border-gray-400/40 p-3 hover:bg-gray-100'>
                  <th className='col-span-2 text-start'>Numero ordine</th>
                  <th className='col-span-3 text-start'>Cliente</th>
                  <th className='col-span-2 text-start'>Data</th>
                  <th className='col-span-2 text-start'>Totale</th>
                  <th className='col-span-2 text-start'>Stato</th>
                  <th className='col-span-1 text-center'>Azioni</th>
                </tr>
              </thead>

              <tbody>
                {filteredOrders.map((order) => (
                  <tr
                    key={order.orderNumber}
                    className='grid grid-cols-12 items-center text-sm border-b border-gray-400/40 p-3 hover:bg-gray-100 last-of-type:border-none'
                  >
                    <td className='col-span-2 text-start'>
                      #{order.orderNumber}
                    </td>
                    <td className='col-span-3'>
                      <p className='font-semibold'>
                        {order.user.firstName} {order.user.lastName}
                      </p>
                      <p>{order.user.email}</p>
                    </td>
                    <td className='col-span-2 flex flex-col justify-center items-start gap-1'>
                      <span>
                        {new Date(order.createdAt).toLocaleDateString('it-IT', {
                          year: 'numeric',
                          month: 'long',
                          day: 'numeric',
                        })}
                      </span>
                      <span>
                        {new Date(order.createdAt).toLocaleString('it-IT', {
                          hour: 'numeric',
                          minute: 'numeric',
                        })}
                      </span>
                    </td>
                    <td className='col-span-2'>
                      {order.totalPrice.toFixed(2).replace('.', ',')} €
                    </td>
                    <td className='col-span-2'>
                      <span
                        className={`text-xs rounded-full px-3 py-0.5 ${getStateStyle(
                          order.state
                        )}`}
                      >
                        {order.state}
                      </span>
                    </td>
                    <td className='col-span-1 flex justify-center items-center'>
                      <Cog
                        className='w-6 h-6 cursor-pointer'
                        onClick={() => {
                          setSelectedOrder(order);
                          setIsBannerOpen(true);
                        }}
                      />
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>

            {selectedOrder !== null && (
              <BottomBanner
                isOpen={isBannerOpen}
                onClose={() => setIsBannerOpen(false)}
              >
                <>
                  <h3 className='text-lg font-semibold tracking-tight'>
                    Ordine #{selectedOrder.orderNumber}
                  </h3>
                  <p className='text-sm text-gray-500 mb-6'>
                    {new Date(selectedOrder.createdAt).toLocaleDateString(
                      'it-IT',
                      {
                        year: 'numeric',
                        month: 'long',
                        day: 'numeric',
                      }
                    )}
                  </p>

                  <div className='flex justify-between items-start mb-6'>
                    <div>
                      <p className='text-sm text-gray-500 font-medium tracking-tight mb-2'>
                        Cliente
                      </p>
                      <p className='font-semibold'>
                        {selectedOrder.user.firstName}{' '}
                        {selectedOrder.user.lastName}
                      </p>
                      <p className='text-sm font-medium'>
                        {selectedOrder.user.email}
                      </p>
                    </div>
                    <div>
                      <p className='text-sm text-gray-500 font-medium tracking-tight mb-2 text-end'>
                        Stato
                      </p>
                      <select
                        className='rounded-md border border-gray-300 bg-white py-2 px-4 pr-8 shadow-sm focus:border-primary focus:ring-primary focus:outline-none focus:ring-1 text-gray-700 text-sm'
                        value={selectedOrder.state}
                        onChange={(e) => {
                          editOrderState(selectedOrder.orderId, e.target.value);
                          setSelectedOrder({
                            ...selectedOrder,
                            state: e.target.value,
                          });
                        }}
                      >
                        {stateOption.map((state) => (
                          <option value={state.option} key={state.id}>
                            {state.option}
                          </option>
                        ))}
                      </select>
                    </div>
                  </div>

                  <div className='w-full'>
                    <h4 className='font-medium'>Dettagli ordine</h4>
                    <table className='w-full'>
                      <thead>
                        <tr className='grid grid-cols-12 text-gray-500 text-sm font-normal border-b border-gray-400/40 p-3 hover:bg-gray-100'>
                          <th className='col-span-7 text-start'>Esperienza</th>
                          <th className='col-span-1 text-end'>Quantità</th>
                          <th className='col-span-2 text-end'>Prezzo</th>
                          <th className='col-span-2 text-end'>Totale</th>
                        </tr>
                      </thead>

                      <tbody>
                        {selectedOrder.orderExperiences.map((exp) => (
                          <tr
                            key={exp.orderExperienceId}
                            className='grid grid-cols-12 text-sm border-b border-gray-400/40 p-3 hover:bg-gray-100'
                          >
                            <td className='col-span-7 text-start'>
                              {exp.title}
                            </td>
                            <td className='col-span-1 text-end'>
                              {exp.quantity}
                            </td>
                            <td className='col-span-2 text-end'>
                              {exp.unitPrice.toFixed(2).replace('.', ',')} €
                            </td>
                            <td className='col-span-2 text-end'>
                              {exp.totalPrice.toFixed(2).replace('.', ',')} €
                            </td>
                          </tr>
                        ))}
                      </tbody>

                      <tfoot>
                        {selectedOrder.shippingPrice > 0 && (
                          <tr className='grid grid-cols-12 text-gray-500 font-medium p-3 hover:bg-gray-100'>
                            <td className='col-span-10 text-end'>
                              Spedizione:
                            </td>
                            <td className='col-span-2 text-end'>
                              {selectedOrder.shippingPrice
                                .toFixed(2)
                                .replace('.', ',')}{' '}
                              €
                            </td>
                          </tr>
                        )}

                        {selectedOrder.totalDiscount > 0 && (
                          <tr className='grid grid-cols-12 text-gray-500 font-medium p-3 hover:bg-gray-100'>
                            <td className='col-span-10 text-end'>
                              Sconto coupon:
                            </td>
                            <td className='col-span-2 text-end'>
                              -
                              {selectedOrder.totalDiscount
                                .toFixed(2)
                                .replace('.', ',')}{' '}
                              €
                            </td>
                          </tr>
                        )}

                        <tr className='grid grid-cols-12 font-bold p-3 hover:bg-gray-100'>
                          <td className='col-span-10 text-end'>
                            Totale ordine
                          </td>
                          <td className='col-span-2 text-end'>
                            {selectedOrder.totalPrice
                              .toFixed(2)
                              .replace('.', ',')}{' '}
                            €
                          </td>
                        </tr>
                      </tfoot>
                    </table>
                  </div>
                </>
              </BottomBanner>
            )}
          </div>
        ) : (
          <div className='flex flex-col justify-center items-center gap-2 py-10'>
            <h3 className='text-xl font-semibold'>Nessun ordine trovato</h3>
            <p className='text-gray-500 font-normal'>
              Non è stato effettuato ancora nessun ordine.
            </p>
          </div>
        )}
      </div>
    </>
  );
};

export default DashboardAdmin;

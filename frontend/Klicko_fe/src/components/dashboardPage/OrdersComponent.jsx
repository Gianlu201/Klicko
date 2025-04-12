import { Calendar, Package, ShoppingBag, TicketX } from 'lucide-react';
import React from 'react';

const OrdersComponent = () => {
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
      <div className='border border-gray-400/40 shadow-sm rounded-xl'>
        <div className='flex flex-col justify-center items-center gap-2 py-10'>
          <h3 className='text-xl font-semibold'>Nessun ordine trovato</h3>
          <p className='text-gray-500 font-normal'>
            Non hai ancora effettuato nessun ordine.
          </p>
        </div>
      </div>
    </>
  );
};

export default OrdersComponent;

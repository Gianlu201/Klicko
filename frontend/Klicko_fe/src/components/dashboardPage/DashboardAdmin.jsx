import {
  BanknoteArrowUp,
  Calendar,
  Funnel,
  Package,
  Search,
  ShoppingBag,
  Users,
} from 'lucide-react';
import React from 'react';
import Button from '../ui/Button';

const DashboardAdmin = () => {
  const options = [
    {
      id: 1,
      title: 'Totale ordini',
      value: 0,
      icon: <ShoppingBag className='text-primary/40 w-8 h-8' />,
    },
    {
      id: 2,
      title: 'Fatturato totale',
      value: 0,
      icon: <BanknoteArrowUp className='text-green-500/40 w-8 h-8' />,
    },
    {
      id: 3,
      title: 'Clienti',
      value: 0,
      icon: <Users className='text-indigo-500/40 w-8 h-8' />,
    },
    {
      id: 4,
      title: 'In attesa',
      value: 0,
      icon: <Calendar className='text-amber-500/40 w-8 h-8' />,
    },
  ];

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
            placeholder='Cerca esperienze...'
            className='bg-background border border-gray-400/30 rounded-xl py-2 ps-10 w-full'
          />
          <Search className='absolute top-1/2 left-3 -translate-y-1/2 w-5 h-5 pb-0.5' />
        </div>

        <Button variant='outline' icon={<Funnel className='w-4 h-4' />}>
          Filtri
        </Button>
      </div>

      {/* tabella storico ordini */}
      <div className='border border-gray-400/40 shadow-sm rounded-xl'>
        <div className='flex flex-col justify-center items-center gap-2 py-10'>
          <h3 className='text-xl font-semibold'>Nessun ordine trovato</h3>
          <p className='text-gray-500 font-normal'>
            Non è stato effettuato ancora nessun ordine.
          </p>
        </div>
      </div>
    </>
  );
};

export default DashboardAdmin;

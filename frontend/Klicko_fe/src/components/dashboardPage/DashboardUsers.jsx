import { Funnel, Search } from 'lucide-react';
import React from 'react';
import Button from '../ui/Button';

const DashboardUsers = () => {
  return (
    <>
      <h2 className='text-2xl font-bold mb-2'>Gestione utenti</h2>
      <p className='text-gray-500 font-normal mb-6'>
        Visualizza e modifica i ruoli degli utenti
      </p>

      <div className='flex justify-between items-center gap-4 mb-8'>
        <div className='relative grow'>
          <input
            type='text'
            placeholder='Cerca utenti...'
            className='bg-background border border-gray-400/30 rounded-xl py-2 ps-10 w-full'
          />
          <Search className='absolute top-1/2 left-3 -translate-y-1/2 w-5 h-5 pb-0.5' />
        </div>

        <Button variant='outline' icon={<Funnel className='w-4 h-4' />}>
          Filtri
        </Button>
      </div>

      {/* tabella utenti registrati */}
      <div className='border border-gray-400/40 shadow-sm rounded-xl'>
        <div className='flex flex-col justify-center items-center gap-2 py-10'>
          <h3 className='text-xl font-semibold'>Nessun utente trovato</h3>
          <p className='text-gray-500 font-normal'>
            Non è stato trovato nessun utente.
          </p>
        </div>
      </div>
    </>
  );
};

export default DashboardUsers;

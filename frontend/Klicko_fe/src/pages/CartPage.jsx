import React from 'react';
import Button from '../components/ui/Button';
import { useNavigate } from 'react-router-dom';
import { ShoppingCart } from 'lucide-react';

const CartPage = () => {
  const navigate = useNavigate();

  return (
    <div className='max-w-7xl mx-auto min-h-screen'>
      <h1 className='text-4xl font-bold mt-10 mb-8'>Carrello</h1>
      <div className='flex flex-col min-h-[60vh] justify-center items-center bg-white rounded-2xl shadow-lg'>
        <ShoppingCart className='w-20 h-20 text-gray-400/60 mb-3' />
        <h3 className='text-xl font-semibold mb-3'>Il tuo carrello è vuoto</h3>
        <p className='text-gray-500 mb-5'>
          Non hai ancora aggiunto esperienze al tuo carrello
        </p>
        <Button
          variant='primary'
          onClick={() => {
            navigate('/experiences');
          }}
        >
          Sfoglia le esperienze
        </Button>
      </div>
    </div>
  );
};

export default CartPage;

import { SquareUserRound, Users } from 'lucide-react';
import Button from '../components/ui/Button';
import { Link, useNavigate } from 'react-router-dom';
import { useEffect } from 'react';
import { useSelector } from 'react-redux';

const RegisterPage = () => {
  const profile = useSelector((state) => {
    return state.profile;
  });

  const navigate = useNavigate();

  useEffect(() => {
    if (profile?.email) {
      navigate('/');
    }
  });

  return (
    <div className='grid grid-cols-2 max-w-7xl mx-auto min-h-screen pt-14 pb-20 items-center gap-12'>
      <div>
        <h1 className='text-5xl font-bold mb-3'>Unisciti a Klicko</h1>
        <p className='text-gray-500 text-lg mb-3'>
          Crea un account per scoprire esperienze uniche e iniziare il tuo
          viaggio di avventure.
        </p>
        <div className='flex gap-5'>
          <div className='bg-white rounded-xl shadow-sm px-5 py-4'>
            <span className='inline-block bg-primary/20 text-primary p-2 rounded-full'>
              <SquareUserRound />
            </span>
            <h4 className='text-lg font-semibold mb-1'>Prenota esperienze</h4>
            <p className='text-sm text-gray-500'>
              Trova e prenota la tua prossima avventura in pochi click
            </p>
          </div>

          <div className='bg-white rounded-xl shadow-sm px-5 py-4'>
            <span className='inline-block bg-secondary/20 text-secondary p-2 rounded-full'>
              <Users />
            </span>
            <h4 className='text-lg font-semibold mb-1'>Prenota esperienze</h4>
            <p className='text-sm text-gray-500'>
              Trova e prenota la tua prossima avventura in pochi click
            </p>
          </div>
        </div>
      </div>

      <div className='bg-white rounded-2xl shadow-xl p-6 mx-auto w-md'>
        <h2 className='text-xl font-bold mb-2'>Crea un account</h2>
        <p className='text-gray-500 mb-5'>
          Inserisci i tuoi dati per registrarti
        </p>
        <form className='mb-5'>
          <div className='flex flex-col mb-4'>
            <label className='mb-2'>Nome</label>
            <input
              type='text'
              placeholder='Inserisci il tuo nome..'
              className='bg-background border border-gray-600/20 rounded-lg py-1.5 px-3'
            />
          </div>
          <div className='flex flex-col mb-4'>
            <label className='mb-2'>Cognome</label>
            <input
              type='text'
              placeholder='Inserisci il tuo cognome..'
              className='bg-background border border-gray-600/20 rounded-lg py-1.5 px-3'
            />
          </div>
          <div className='flex flex-col mb-4'>
            <label className='mb-2'>Email</label>
            <input
              type='text'
              placeholder='Inserisci la tua email..'
              className='bg-background border border-gray-600/20 rounded-lg py-1.5 px-3'
            />
          </div>
          <div className='flex flex-col mb-4'>
            <label className='mb-2'>Password</label>
            <input
              type='password'
              placeholder='Inserisci la tua password..'
              className='bg-background border border-gray-600/20 rounded-lg py-1.5 px-3'
            />
          </div>
          <div className='flex flex-col mb-6'>
            <label className='mb-2'>Conferma password</label>
            <input
              type='text'
              placeholder='Conferma la tua password..'
              className='bg-background border border-gray-600/20 rounded-lg py-1.5 px-3'
            />
          </div>
          <Button variant='primary' fullWidth={true}>
            Registrati
          </Button>
        </form>

        <div className='flex justify-center gap-1 text-sm'>
          <p className='text-gray-500'>Hai già un account?</p>
          <Link to='/login' className='text-primary hover:underline'>
            Accedi
          </Link>
        </div>
      </div>
    </div>
  );
};

export default RegisterPage;

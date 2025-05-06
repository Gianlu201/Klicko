import { SquareUserRound, Users } from 'lucide-react';
import Button from '../components/ui/Button';
import { Link, useNavigate } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import { toast } from 'sonner';

const RegisterPage = () => {
  const backendUrl = import.meta.env.VITE_BACKEND_URL;

  const [name, setName] = useState('');
  const [surname, setSurname] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [errorMessage, setErrorMessage] = useState('');
  const [showErrorMessage, setShowErrorMessage] = useState(false);

  const profile = useSelector((state) => {
    return state.profile;
  });

  const navigate = useNavigate();

  const checkFormValidity = (e) => {
    e.preventDefault();
    if (
      name.length > 0 &&
      surname.length > 0 &&
      email.length > 0 &&
      password.length > 0 &&
      confirmPassword.length > 0
    ) {
      sendRegistrationForm();
    } else {
      setShowErrorMessage(true);
    }
  };

  const sendRegistrationForm = async () => {
    try {
      setErrorMessage('');

      if (password !== confirmPassword) {
        setErrorMessage('Le password non coincidono!');
      } else {
        const body = {
          firstName: name,
          lastName: surname,
          email: email,
          password: password,
        };

        const response = await fetch(`${backendUrl}/Account/register`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(body),
        });
        if (response.ok) {
          toast.success(
            <>
              <p className='font-bold'>Registrazione completata!</p>
              <p>Registrazione avvenuta con successo</p>
            </>
          );
          navigate('/login');
        } else {
          throw new Error('Qualcosa è andato storto, riprova!');
        }
      }
    } catch (e) {
      toast.error(
        <>
          <p className='font-bold'>Errore nella registrazione!</p>
          <p>{e.message}</p>
        </>
      );
    }
  };

  useEffect(() => {
    if (profile?.email) {
      navigate('/');
    }
  });

  return (
    <div className='lg:grid lg:grid-cols-2 max-w-7xl mx-auto min-h-screen pt-14 pb-20 items-center gap-12 px-6 xl:px-0'>
      <div className='mb-10'>
        <h1 className='text-5xl font-bold mb-3 md:max-lg:text-center'>
          Unisciti a Klicko
        </h1>
        <p className='text-gray-500 text-lg mb-3 md:max-lg:text-center md:max-lg:max-w-3/5 md:max-lg:mx-auto'>
          Crea un account per scoprire esperienze uniche e iniziare il tuo
          viaggio di avventure.
        </p>
        <div className='sm:flex md:flex-col md:max-lg:items-center gap-5'>
          <div className='bg-white rounded-xl shadow-sm px-5 py-4 md:max-lg:w-3/5 mb-4 sm:mb-0'>
            <span className='inline-block bg-primary/20 text-primary p-2 rounded-full'>
              <SquareUserRound />
            </span>
            <h4 className='text-lg font-semibold mb-1'>Prenota esperienze</h4>
            <p className='text-sm text-gray-500'>
              Trova e prenota la tua prossima avventura in pochi click
            </p>
          </div>

          <div className='bg-white rounded-xl shadow-sm px-5 py-4 md:max-lg:w-3/5'>
            <span className='inline-block bg-secondary/20 text-secondary p-2 rounded-full'>
              <Users />
            </span>
            <h4 className='text-lg font-semibold mb-1'>Gestisci il profilo</h4>
            <p className='text-sm text-gray-500'>
              Tieni traccia delle tue prenotazioni e preferenze
            </p>
          </div>
        </div>
      </div>

      <div className='bg-white rounded-2xl shadow-xl p-6 mx-auto w-full max-w-md'>
        <h2 className='text-xl font-bold mb-2'>Crea un account</h2>
        <p className='text-gray-500 mb-5'>
          Inserisci i tuoi dati per registrarti
        </p>
        <form
          className='mb-5'
          onSubmit={(e) => {
            checkFormValidity(e);
          }}
        >
          <div className='flex flex-col mb-4'>
            <label className='mb-2'>Nome</label>
            <input
              type='text'
              placeholder='Inserisci il tuo nome..'
              className='text-sm sm:text-base bg-background border border-gray-600/20 rounded-lg py-1.5 px-3'
              value={name}
              onChange={(e) => {
                setName(e.target.value);
              }}
            />
            {showErrorMessage && name.length === 0 && (
              <span className='text-sm text-red-500 mt-1'>
                Questo campo è obbligatorio!
              </span>
            )}
          </div>

          <div className='flex flex-col mb-4'>
            <label className='mb-2'>Cognome</label>
            <input
              type='text'
              placeholder='Inserisci il tuo cognome..'
              className='text-sm sm:text-base bg-background border border-gray-600/20 rounded-lg py-1.5 px-3'
              value={surname}
              onChange={(e) => {
                setSurname(e.target.value);
              }}
            />
            {showErrorMessage && surname.length === 0 && (
              <span className='text-sm text-red-500 mt-1'>
                Questo campo è obbligatorio!
              </span>
            )}
          </div>

          <div className='flex flex-col mb-4'>
            <label className='mb-2'>Email</label>
            <input
              type='email'
              placeholder='Inserisci la tua email..'
              className='text-sm sm:text-base bg-background border border-gray-600/20 rounded-lg py-1.5 px-3'
              value={email}
              onChange={(e) => {
                setEmail(e.target.value);
              }}
            />
            {showErrorMessage && email.length === 0 && (
              <span className='text-sm text-red-500 mt-1'>
                Questo campo è obbligatorio!
              </span>
            )}
          </div>

          <div className='flex flex-col mb-4'>
            <label className='mb-2'>Password</label>
            <input
              type='password'
              placeholder='Inserisci la tua password..'
              className='text-sm sm:text-base bg-background border border-gray-600/20 rounded-lg py-1.5 px-3'
              value={password}
              onChange={(e) => {
                setPassword(e.target.value);
              }}
            />
            {showErrorMessage && password.length === 0 && (
              <span className='text-sm text-red-500 mt-1'>
                Questo campo è obbligatorio!
              </span>
            )}
          </div>

          <div className='flex flex-col mb-6'>
            <label className='mb-2'>Conferma password</label>
            <input
              type='password'
              placeholder='Conferma la tua password..'
              className='text-sm sm:text-base bg-background border border-gray-600/20 rounded-lg py-1.5 px-3'
              value={confirmPassword}
              onChange={(e) => {
                setConfirmPassword(e.target.value);
              }}
            />
            {showErrorMessage && confirmPassword.length === 0 && (
              <span className='text-sm text-red-500 mt-1'>
                Questo campo è obbligatorio!
              </span>
            )}
          </div>

          {errorMessage !== '' && (
            <p className='text-sm text-red-500 my-2'>{errorMessage}</p>
          )}

          <Button type='submit' variant='primary' fullWidth={true}>
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

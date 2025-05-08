import React, { useEffect, useState } from 'react';
import Button from '../components/ui/Button';
import { Link, useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { jwtDecode } from 'jwt-decode';
import { toast } from 'sonner';

const LoginPage = () => {
  const backendUrl = import.meta.env.VITE_BACKEND_URL;

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [showErrorMessage, setShowErrorMessage] = useState(false);
  const [credentialsError, setCredentialsError] = useState(false);

  const profile = useSelector((state) => {
    return state.profile;
  });

  const dispatch = useDispatch();

  const navigate = useNavigate();

  const checkFormValidity = () => {
    setCredentialsError(false);
    if (email.length === 0 || password.length === 0) {
      setShowErrorMessage(true);
    } else {
      sendLoginForm();
    }
  };

  const sendLoginForm = async () => {
    try {
      const body = {
        email: email,
        password: password,
      };

      const response = await fetch(`${backendUrl}/Account/login`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(body),
      });
      if (response.ok) {
        const data = await response.json();

        if (data.token !== null) {
          localStorage.setItem('klicko_token', JSON.stringify(data));
          setCurrentProfile(data);
          navigate('/');
        } else {
          throw new Error();
        }
      } else if (response.status === 400) {
        setCredentialsError(true);
      } else {
        throw new Error();
      }
    } catch {
      toast.error(
        <>
          <p className='font-bold'>Errore nel login!</p>
          <p>Qualcosa è andato storto, riprova!</p>
        </>
      );
    }
  };

  const setCurrentProfile = (loginData) => {
    const tokenDecoded = jwtDecode(loginData.token);

    const userInfos = {
      aud: tokenDecoded.aud,
      exp: tokenDecoded.exp,
      role: tokenDecoded[
        'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
      ],
      email:
        tokenDecoded[
          'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'
        ],
      fullName:
        tokenDecoded[
          'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
        ],
      id: tokenDecoded[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
      ],
      iss: tokenDecoded.iss,
      expiration: loginData.expires,
    };

    dispatch({
      type: 'SET_LOGGED_USER',
      payload: userInfos,
    });
  };

  useEffect(() => {
    if (profile?.email) {
      navigate('/');
    }
  });

  return (
    <div className='flex flex-col items-center justify-center min-h-screen gap-5 px-6'>
      <h1 className='relative text-4xl font-bold -translate-y-10 sm:text-5xl'>
        Bentornato su Klicko
      </h1>

      <div className='relative max-w-lg p-4 mx-auto -translate-y-10 bg-white shadow-xl rounded-2xl sm:p-6'>
        <h2 className='mb-2 text-xl font-bold'>Accedi</h2>
        <p className='mb-5 text-gray-500'>
          Inserisci le tue credenziali per accedere al tuo account
        </p>
        <form className='mb-3'>
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
              <span className='mt-1 text-sm text-red-500'>
                Questo campo è obbligatorio!
              </span>
            )}
          </div>

          <div className='flex flex-col mb-5'>
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
              <span className='mt-1 text-sm text-red-500'>
                Questo campo è obbligatorio!
              </span>
            )}
          </div>

          {credentialsError && (
            <p className='px-3 py-4 mb-4 text-sm font-medium text-center text-red-600 bg-red-200/60 rounded-xl'>
              Email o password non corretti, riprovare!
            </p>
          )}

          <Button
            type='button'
            variant='primary'
            fullWidth={true}
            onClick={checkFormValidity}
          >
            Accedi
          </Button>
        </form>

        <div className='flex justify-center gap-1 text-sm'>
          <p className='text-gray-500'>Non hai un account?</p>
          <Link to='/register' className='text-primary hover:underline'>
            Registrati
          </Link>
        </div>
      </div>
    </div>
  );
};

export default LoginPage;

import React, { useEffect, useState } from 'react';
import Button from '../components/ui/Button';
import { Link, useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { jwtDecode } from 'jwt-decode';

const LoginPage = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const profile = useSelector((state) => {
    return state.profile;
  });

  const dispatch = useDispatch();

  const navigate = useNavigate();

  const sendLoginForm = async () => {
    try {
      const body = {
        email: email,
        password: password,
      };

      const response = await fetch('https://localhost:7235/api/Account/login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(body),
      });
      if (response.ok) {
        const data = await response.json();
        console.log(data);

        if (data.token !== null) {
          localStorage.setItem('klicko_token', JSON.stringify(data));
          setCurrentProfile(data);
          navigate('/');
        } else {
          throw new Error();
        }
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch {
      console.log('Error');
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

    console.log(userInfos);

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
    <div className='flex flex-col justify-center items-center gap-5 min-h-screen'>
      <h1 className='text-5xl font-bold'>Bentornato su Klicko</h1>

      <div className='bg-white rounded-2xl shadow-xl p-6 mx-auto max-w-lg'>
        <h2 className='text-xl font-bold mb-2'>Accedi</h2>
        <p className='text-gray-500 mb-5'>
          Inserisci le tue credenziali per accedere al tuo account
        </p>
        <form className='mb-3'>
          <div className='flex flex-col mb-4'>
            <label className='mb-2'>Email</label>
            <input
              type='text'
              placeholder='Inserisci la tua email..'
              className='bg-background border border-gray-600/20 rounded-lg py-1.5 px-3'
              value={email}
              onChange={(e) => {
                setEmail(e.target.value);
              }}
            />
          </div>
          <div className='flex flex-col mb-5'>
            <label className='mb-2'>Password</label>
            <input
              type='password'
              placeholder='Inserisci la tua password..'
              className='bg-background border border-gray-600/20 rounded-lg py-1.5 px-3'
              value={password}
              onChange={(e) => {
                setPassword(e.target.value);
              }}
            />
          </div>
          <Button
            type='button'
            variant='primary'
            fullWidth={true}
            onClick={sendLoginForm}
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

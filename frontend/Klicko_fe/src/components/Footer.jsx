import { Facebook, Github, Instagram, Linkedin, Twitter } from 'lucide-react';
import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import Button from './ui/Button';
import { useDispatch } from 'react-redux';
import { setSelectedCategoryName } from '../redux/actions';
import { toast } from 'sonner';

const Footer = () => {
  const backendUrl = import.meta.env.VITE_BACKEND_URL;

  const [newsLetterEmail, setNewsLetterEmail] = useState('');

  const dispatch = useDispatch();

  const experienceOptions = [
    {
      id: 1,
      title: 'Esperienze in Aria',
      name: 'Aria',
      url: '/experiences',
    },
    {
      id: 2,
      title: 'Esperienze in Acqua',
      name: 'Acqua',
      url: '/experiences',
    },
    {
      id: 3,
      title: 'Trekking e Avventure',
      name: 'Trekking',
      url: '/experiences',
    },
    {
      id: 4,
      title: 'Sport e Motori',
      name: 'Motori',
      url: '/experiences',
    },
    {
      id: 5,
      title: 'Gastronomia',
      name: 'Gastronomia',
      url: '/experiences',
    },
  ];

  const infoOptions = [
    {
      id: 1,
      title: 'Chi siamo',
      url: '/about',
    },
    {
      id: 2,
      title: 'FAQ',
      url: '/faq',
    },
    {
      id: 3,
      title: 'Termini e Condizioni',
      url: '/termsAndConditions',
    },
    {
      id: 4,
      title: 'Privacy Policy',
      url: '/privacyPolicy',
    },
    {
      id: 5,
      title: 'Contattaci',
      url: '/contact',
    },
    {
      id: 6,
      title: 'Riscatta Voucher',
      url: '/redeemVoucher',
    },
  ];

  const sendNewsLetterEmail = async () => {
    try {
      const response = await fetch(`${backendUrl}/Email/sendNewsLetter`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(newsLetterEmail),
      });
      if (response.ok) {
        toast.success(
          <>
            <p className='font-bold'>Iscrizione confermata!</p>
            <p>Benvenuto nella nostra newsletter!</p>
          </>
        );
        setNewsLetterEmail('');
      } else {
        throw new Error(`Errore nell'iscrizione alla newsletter`);
      }
    } catch (e) {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>{e.message}</p>
        </>
      );
      setNewsLetterEmail('');
    }
  };

  return (
    <div className='py-10 bg-slate-950'>
      <div className='gap-6 px-6 mx-auto mb-8 text-gray-500 xl:px-0 max-w-7xl md:flex'>
        <div className='mb-6 md:w-1/4 pe-6'>
          <h3 className='mb-4 text-2xl font-semibold text-white'>Klicko</h3>
          <p className='mb-4'>
            Trova e prenota esperienze uniche in tutto il mondo. Rendi ogni
            viaggio indimenticabile con le nostre avventure selezionate.
          </p>
          <div className='flex gap-3'>
            <Facebook className='cursor-pointer hover:text-white' />
            <Instagram className='cursor-pointer hover:text-white' />
            <Twitter className='cursor-pointer hover:text-white' />
            <a
              href='https://github.com/Gianlu201'
              className='cursor-pointer hover:text-white'
              target='_blank'
            >
              <Github />
            </a>
            <a
              href='https://www.linkedin.com/in/gianluca-di-diego-a3604716b/'
              className='cursor-pointer hover:text-white'
              target='_blank'
            >
              <Linkedin />
            </a>
          </div>
        </div>

        <div className='mb-6 md:w-1/4'>
          <h4 className='mb-4 text-lg font-semibold text-white'>Esperienze</h4>
          <ul>
            {experienceOptions.map((exp) => (
              <li key={exp.id} className='mb-2'>
                <Link
                  to={exp.url}
                  className='hover:text-white'
                  onClick={() => {
                    dispatch(setSelectedCategoryName(exp.name));
                  }}
                >
                  {exp.title}
                </Link>
              </li>
            ))}
          </ul>
        </div>

        <div className='mb-6 md:w-1/4'>
          <h4 className='mb-4 text-lg font-semibold text-white'>
            Informazioni
          </h4>
          <ul>
            {infoOptions.map((info) => (
              <li key={info.id} className='mb-2'>
                <Link to={info.url} className='hover:text-white'>
                  {info.title}
                </Link>
              </li>
            ))}
          </ul>
        </div>

        <div className='md:w-1/4'>
          <h4 className='mb-4 text-lg font-semibold text-white'>Newsletter</h4>
          <p className='mb-4'>
            Iscriviti per ricevere offerte speciali e scoprire nuove esperienze
          </p>
          <form
            onSubmit={(e) => {
              e.preventDefault();
              sendNewsLetterEmail();
            }}
          >
            <input
              type='email'
              placeholder='La tua email'
              className='w-full py-2 mb-2 bg-gray-800 border border-gray-700 rounded-lg text-gray-50 ps-4'
              value={newsLetterEmail}
              onChange={(e) => setNewsLetterEmail(e.target.value)}
              required
            />
            <Button
              type='submit'
              variant='primary'
              fullWidth={true}
              disabled={newsLetterEmail === '' ? true : false}
            >
              Iscriviti
            </Button>
          </form>
        </div>
      </div>
      <div className='justify-between pt-4 mx-6 text-gray-500 border-t md:flex max-w-7xl lg:mx-auto border-t-gray-600'>
        <p className='mb-3 md:mb-0'>
          &copy; 2025 Klicko. Tutti i diritti riservati
        </p>
        <div className='flex gap-2'>
          <Link to='/termsAndConditions' className='hover:text-white'>
            Termini
          </Link>
          <Link to='/privacyPolicy' className='hover:text-white'>
            Privacy
          </Link>
        </div>
      </div>
    </div>
  );
};

export default Footer;

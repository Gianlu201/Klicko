import { Facebook, Instagram, Twitter } from 'lucide-react';
import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import Button from './ui/Button';
import { useDispatch } from 'react-redux';
import { setSelectedCategoryName } from '../redux/actions';
import { toast } from 'sonner';

const Footer = () => {
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
      const response = await fetch(
        `https://localhost:7235/api/Email/sendNewsLetter`,
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(newsLetterEmail),
        }
      );
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
    <div className='bg-slate-950 py-10'>
      <div className='px-6 xl:px-0 max-w-7xl mx-auto md:flex gap-6 text-gray-500 mb-8'>
        <div className='md:w-1/4 pe-6 mb-6'>
          <h3 className='text-white text-2xl font-semibold mb-4'>Klicko</h3>
          <p className='mb-4'>
            Trova e prenota esperienze uniche in tutto il mondo. Rendi ogni
            viaggio indimenticabile con le nostre avventure selezionate.
          </p>
          <div className='flex gap-3'>
            <Facebook className='cursor-pointer hover:text-white' />
            <Instagram className='cursor-pointer hover:text-white' />
            <Twitter className='cursor-pointer hover:text-white' />
          </div>
        </div>

        <div className='md:w-1/4 mb-6'>
          <h4 className='text-white text-lg font-semibold mb-4'>Esperienze</h4>
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

        <div className='md:w-1/4 mb-6'>
          <h4 className='text-white text-lg font-semibold mb-4'>
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
          <h4 className='text-white text-lg font-semibold mb-4'>Newsletter</h4>
          <p className='mb-4'>
            Iscriviti per ricevere offerte speciali e scoprire nuove esperienze
          </p>
          <input
            type='email'
            placeholder='La tua email'
            className='bg-gray-800 w-full text-gray-50 py-2 ps-4 rounded-lg border border-gray-700 mb-2'
            value={newsLetterEmail}
            onChange={(e) => setNewsLetterEmail(e.target.value)}
          />
          <Button
            variant='primary'
            fullWidth={true}
            disabled={newsLetterEmail === '' ? true : false}
            onClick={() => sendNewsLetterEmail()}
          >
            Iscriviti
          </Button>
        </div>
      </div>
      <div className='mx-6 flex justify-between max-w-7xl lg:mx-auto border-t border-t-gray-600 text-gray-500 pt-4'>
        <p>&copy; 2025 Klicko. Tutti i diritti riservati</p>
        <div className='flex gap-2'>
          <Link to='/termsAndConditions' className='hover:text-white'>
            Termini
          </Link>
          <Link to='/privacyPolicy' className='hover:text-white'>
            Privacy
          </Link>
          <Link to='/#' className='hover:text-white'>
            Cookie
          </Link>
        </div>
      </div>
    </div>
  );
};

export default Footer;

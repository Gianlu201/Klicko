import {
  Calendar,
  Clock,
  HandCoins,
  MapPin,
  Shield,
  ShieldOff,
  Tag,
  User,
  UsersRound,
} from 'lucide-react';
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Carousel from '../components/ui/Corousel';
import Button from '../components/ui/Button';
import { useDispatch, useSelector } from 'react-redux';
import { cartModified } from '../redux/actions';
import { toast } from 'sonner';

const DetailPage = () => {
  const [experience, setExperience] = useState({});
  const [description, setDescription] = useState(true);
  const [foto, setFoto] = useState(false);
  const [info, setInfo] = useState(false);

  const cart = useSelector((state) => {
    return state.cart;
  });

  const dispatch = useDispatch();

  const params = useParams();

  const getSelectedExperience = async () => {
    try {
      const response = await fetch(
        `https://localhost:7235/api/Experience/Experience/${params.experienceId}`,
        {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
          },
        }
      );
      if (response.ok) {
        const data = await response.json();
        // console.log(data);

        setExperience(data.experience);
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch (e) {
      console.log(e.message);
    }
  };

  const handleTabs = (target) => {
    switch (target) {
      case 'description':
        setDescription(true);
        setFoto(false);
        setInfo(false);
        break;

      case 'foto':
        setDescription(false);
        setFoto(true);
        setInfo(false);
        break;

      case 'info':
        setDescription(false);
        setFoto(false);
        setInfo(true);
        break;

      default:
        break;
    }
  };

  const addExperienceToCart = async (experienceId) => {
    try {
      const response = await fetch(
        `https://localhost:7235/api/Cart/AddExperience/${cart.cartId}`,
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(experienceId),
        }
      );
      if (response.ok) {
        toast.success(
          <>
            <p className='font-bold'>Esperienza aggiunta al carrello!</p>
            <p>{experience.title}</p>
          </>
        );
        dispatch(cartModified());
      } else {
        throw new Error(`Impossibile aggiungere l'esperienza al carrello`);
      }
    } catch (e) {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>{e.message}</p>
        </>
      );
    }
  };

  useEffect(() => {
    getSelectedExperience();
  }, []);

  return (
    <div>
      {experience?.title != null ? (
        <div>
          <div className='relative h-[30vh] md:h-[50vh] lg:h-[70vh] overflow-hidden'>
            <img
              src={`https://localhost:7235/uploads/${experience.coverImage}`}
              alt={experience.title}
              className='absolute top-1/2 start-0 -translate-y-1/2  w-full'
            />
            <div className='absolute bg-black/30 w-full h-full'></div>
            <div className='absolute bottom-0 start-10 lg:start-40 z-20 text-white max-w-3/4'>
              <h1 className='text-3xl md:text-4xl lg:text-5xl font-bold mb-4'>
                {experience.title}
              </h1>
              <div className='flex gap-8 mb-8 text-sm md:text-base'>
                <span className='flex items-center gap-2'>
                  <MapPin className='w-4.5 h-4.5' />
                  {experience.place}
                </span>
                <span className='flex items-center gap-2'>
                  <Clock className='w-4.5 h-4.5' />
                  {experience.duration}
                </span>
                <span className='flex items-center gap-2'>
                  <Tag className='w-4.5 h-4.5' />
                  {experience.category.name}
                </span>
              </div>
            </div>
          </div>

          <div className='grid grid-cols-3 gap-4 max-w-7xl mx-auto mb-20 px-6 xl:px-0'>
            {/* first column */}
            <div className='bg-white col-span-3 lg:col-span-2 rounded-b-2xl shadow-lg mb-16'>
              {/* navigation tables */}
              <div className=' inline-block ms-6 px-2 pb-2 bg-gray-100 rounded-b-2xl'>
                <button
                  className={`px-4 py-1 cursor-pointer ${
                    description ? 'bg-white rounded-b-xl' : ''
                  }`}
                  onClick={() => {
                    handleTabs('description');
                  }}
                >
                  Descrizione
                </button>
                <button
                  className={`px-4 py-1 cursor-pointer ${
                    foto ? 'bg-white rounded-b-xl' : ''
                  }`}
                  onClick={() => {
                    handleTabs('foto');
                  }}
                >
                  Foto
                </button>
                <button
                  className={`px-4 py-1 cursor-pointer ${
                    info ? 'bg-white rounded-b-xl' : ''
                  }`}
                  onClick={() => {
                    handleTabs('info');
                  }}
                >
                  Informazioni
                </button>
              </div>

              {/* description table */}
              {description && (
                <div className='px-6 mt-10 mb-8'>
                  <p className='lg:text-xl text-gray-600 mb-3'>
                    {experience.description}
                  </p>

                  <hr className='text-gray-500/50 my-3' />

                  <h4 className='text-xl font-semibold'>Dettagli</h4>

                  <div className='flex gap-2 my-2'>
                    <span className='flex items-center gap-1.5 text-gray-600'>
                      <Clock className='w-4 h-4' />
                      Durata:
                    </span>
                    <span className='font-semibold'>
                      {experience?.duration}
                    </span>
                  </div>
                  <div className='flex gap-2 my-2'>
                    <span className='flex items-center gap-1.5 text-gray-600'>
                      <MapPin className='w-4 h-4' />
                      Luogo:
                    </span>
                    <span className='font-semibold'>{experience?.place}</span>
                  </div>
                  <div className='flex gap-2 my-2'>
                    <span className='flex items-center gap-1.5 text-gray-600'>
                      <User className='w-4 h-4' />
                      Organizzato da:
                    </span>
                    <span className='font-semibold'>
                      {experience?.organiser}
                    </span>
                  </div>
                  <div className='flex gap-2 my-2'>
                    <span className='flex items-center gap-1.5 text-gray-600'>
                      <Calendar className='w-4 h-4' />
                      Aggiornato il:
                    </span>
                    <span className='font-semibold'>
                      {new Date(experience?.lastEditDate).toLocaleDateString(
                        'it-IT',
                        {
                          year: 'numeric',
                          month: 'long',
                          day: 'numeric',
                        }
                      )}
                    </span>
                  </div>
                </div>
              )}

              {/* fotos table */}
              {foto && (
                <div className='px-6 mt-2 mb-8'>
                  {experience?.images.length > 0 && (
                    <Carousel
                      items={experience.images}
                      slidesVisible={2}
                      autoplay={true}
                      delay={5000}
                      className='max-w-6xl mx-auto'
                    />
                  )}
                </div>
              )}

              {/* info table */}
              {info && (
                <div className='px-6 mt-10 mb-8'>
                  <div className='my-3'>
                    <h4 className='text-xl font-semibold mb-3'>
                      Cosa è incluso
                    </h4>
                    <p className='text-gray-600'>
                      {experience?.includedDescription}
                    </p>
                  </div>

                  {experience?.carryWiths &&
                    experience?.carryWiths.length > 0 && (
                      <div className='my-3'>
                        <h4 className='text-xl font-semibold mb-3'>
                          Cosa portare
                        </h4>
                        <ul className='ps-6'>
                          {experience.carryWiths.map((element) => (
                            <li key={element.carryWithId} className='list-disc'>
                              {element.name}
                            </li>
                          ))}
                        </ul>
                      </div>
                    )}

                  <div className='my-3'>
                    <h4 className='text-xl font-semibold mb-3'>
                      Politica di cancellazione
                    </h4>
                    <p className='text-gray-600 flex gap-1.5 items-center'>
                      {experience?.isFreeCancellable ? (
                        <>
                          <Shield className='w-4 h-4 pb-0.5 text-green-600' />
                          <span>
                            Cancellazione gratuita fino a 48 ore prima
                          </span>
                        </>
                      ) : (
                        <>
                          <ShieldOff className='w-4 h-4 pb-0.5 text-red-600' />
                          <span>Cancellazione gratuita non prevista</span>
                        </>
                      )}
                    </p>
                  </div>
                </div>
              )}
            </div>

            {/* second column */}
            <div className='col-span-3 lg:col-span-1'>
              <div className='bg-white rounded-2xl shadow-lg px-6 py-6 -translate-y-10 sticky top-32'>
                <h4 className='text-lg text-gray-500 mb-1'>Prezzo totale</h4>
                <div className='mb-3'>
                  {experience.sale > 0 && (
                    <span className={`font-bold text-3xl me-2`}>
                      {((experience.price * (100 - experience.sale)) / 100)
                        .toFixed(2)
                        .replace('.', ',')}{' '}
                      €
                    </span>
                  )}
                  <span
                    className={`font-bold ${
                      experience.sale > 0
                        ? 'line-through text-gray-500 me-2 text-xl'
                        : 'text-secondary text-3xl'
                    }`}
                  >
                    {experience.price.toFixed(2).replace('.', ',')}€
                  </span>
                </div>

                <Button
                  variant='primary'
                  fullWidth={true}
                  onClick={() => {
                    addExperienceToCart(experience.experienceId);
                  }}
                >
                  Aggiungi al carrello
                </Button>

                <hr className='text-gray-500/30 my-5' />

                <h4 className='font-semibold mb-2'>Informazioni importanti</h4>

                <div className='flex items-center gap-1.5 mb-2'>
                  <UsersRound className='w-4 h-4' />
                  <span>
                    Esperienze per{' '}
                    <span className='font-semibold'>
                      {experience.maxParticipants}
                    </span>
                  </span>
                </div>

                {experience.maxParticipants > 1 && (
                  <div className='flex items-center gap-1.5 mb-2'>
                    <HandCoins className='w-4 h-4' />
                    Costo per persona:
                    <span className='font-semibold'>
                      {(experience.price / experience.maxParticipants)
                        .toFixed(2)
                        .replace('.', ',')}
                      €
                    </span>
                  </div>
                )}

                <div className='flex items-center gap-1.5 mb-2'>
                  <Clock className='w-4 h-4' />
                  Durata complessiva:
                  <span className='font-semibold'>{experience.duration}</span>
                </div>

                <div className='flex items-center gap-1.5 mb-2'>
                  {experience.isFreeCancellable ? (
                    <>
                      <Shield className='w-4 h-4 pb-0.5' />
                      <span>Cancellazione gratuita fino a 48 ore prima</span>
                    </>
                  ) : (
                    <>
                      <ShieldOff className='w-4 h-4 pb-0.5' />
                      <span>Cancellazione gratuita non prevista</span>
                    </>
                  )}
                </div>

                <div className='flex items-center gap-1.5 mb-2'>
                  <Calendar className='w-4 h-4' />
                  Disponibile tutti i giorni
                </div>
              </div>
            </div>
          </div>
        </div>
      ) : (
        <div className='w-full text-2xl text-center text-gray-500 font-semibold py-20 min-h-[60vh]'>
          Esperienza non trovata
        </div>
      )}
    </div>
  );
};

export default DetailPage;

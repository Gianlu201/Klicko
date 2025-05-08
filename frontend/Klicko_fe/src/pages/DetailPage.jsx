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
import { useNavigate, useParams } from 'react-router-dom';
import Carousel from '../components/ui/Corousel';
import Button from '../components/ui/Button';
import { useDispatch, useSelector } from 'react-redux';
import { cartModified, addExperienceToLocalCart } from '../redux/actions';
import { toast } from 'sonner';
import SkeletonCompleteImage from '../components/ui/SkeletonCompleteImage';

const DetailPage = () => {
  const backendUrl = import.meta.env.VITE_BACKEND_URL;

  const [isLoading, setIsLoading] = useState(true);
  const [experience, setExperience] = useState({});
  const [description, setDescription] = useState(true);
  const [foto, setFoto] = useState(false);
  const [info, setInfo] = useState(false);

  const cart = useSelector((state) => {
    return state.cart;
  });

  const navigate = useNavigate();

  const dispatch = useDispatch();

  const params = useParams();

  const getSelectedExperience = async () => {
    try {
      setIsLoading(true);

      const response = await fetch(
        `${backendUrl}/Experience/Experience/${params.experienceId}`,
        {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
          },
        }
      );
      if (response.ok) {
        const data = await response.json();

        setIsLoading(false);
        setExperience(data.experience);
      } else if (response.status === 404) {
        throw new Error('Esperienza non trovata!');
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch (e) {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>{e.message}</p>
        </>
      );
      navigate('/pageNotFound');
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

  const handleAddToCart = (exp) => {
    switch (cart.cartId !== '') {
      case true:
        addExperienceToCart(exp.experienceId);
        break;

      case false:
        addExperienceToLocalCartFunction(exp);
        break;

      default:
        toast.error(
          <>
            <p className='font-bold'>Errore!</p>
            <p>Errore nell'aggiunta al carrello</p>
          </>
        );
        break;
    }
  };

  const addExperienceToCart = async (experienceId) => {
    try {
      const response = await fetch(
        `${backendUrl}/Cart/AddExperience/${cart.cartId}`,
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

  const addExperienceToLocalCartFunction = (experience) => {
    dispatch(addExperienceToLocalCart(experience));
    toast.success(
      <>
        <p className='font-bold'>Esperienza aggiunta al carrello!</p>
        <p>{experience.title}</p>
      </>
    );
  };

  useEffect(() => {
    getSelectedExperience();
  }, []);

  return (
    <div className='min-h-[100vh]'>
      {isLoading ? (
        <div className='w-2/3 mx-auto mt-10 min-w-sm'>
          <SkeletonCompleteImage />
        </div>
      ) : (
        experience?.title != null && (
          <div>
            <div className='relative h-[25vh] xs:h-[30vh] md:h-[50vh] lg:h-[70vh] overflow-hidden'>
              <img
                src={`https://klicko-backend-api.azurewebsites.net/uploads/${experience.coverImage}`}
                alt={experience.title}
                className='absolute w-full -translate-y-1/2 max-md:h-full md:bg-cover top-1/2 start-0'
              />
              <div className='absolute w-full h-full bg-black/30'></div>
              <div className='absolute bottom-0 z-20 text-white start-10 lg:start-40 max-w-3/4'>
                <h1 className='mb-4 text-3xl font-bold md:text-4xl lg:text-5xl line-clamp-2'>
                  {experience.title}
                </h1>
                <div className='flex gap-8 mb-8 text-sm md:text-base'>
                  <span className='flex items-center gap-2'>
                    <MapPin className='w-4.5 h-4.5' />
                    <span className='line-clamp-2'>{experience.place}</span>
                  </span>
                  <span className='flex items-center gap-2'>
                    <Clock className='w-4.5 h-4.5' />
                    {experience.duration}
                  </span>
                  <span className='items-center hidden gap-2 sm:flex'>
                    <Tag className='w-4.5 h-4.5' />
                    {experience.category.name}
                  </span>
                </div>
              </div>
            </div>

            <div className='grid grid-cols-3 gap-4 px-6 mx-auto mb-20 max-w-7xl xl:px-0'>
              {/* first column */}
              <div className='col-span-3 mb-16 bg-white shadow-lg lg:col-span-2 rounded-b-2xl'>
                {/* navigation tables */}
                <div className='inline-block px-2 pb-2 mx-6 bg-gray-100 rounded-b-2xl'>
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
                  {experience.images && (
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
                  )}

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
                    <p className='mb-3 text-gray-600 lg:text-xl'>
                      {experience.description}
                    </p>

                    <hr className='my-3 text-gray-500/50' />

                    <h4 className='text-xl font-semibold'>Dettagli</h4>

                    <div className='gap-2 my-2 sm:flex'>
                      <span className='flex items-center gap-1.5 text-gray-600'>
                        <Clock className='w-4 h-4' />
                        Durata:
                      </span>
                      <span className='font-semibold'>
                        {experience?.duration}
                      </span>
                    </div>
                    <div className='gap-2 my-2 sm:flex'>
                      <span className='flex items-center gap-1.5 text-gray-600'>
                        <MapPin className='w-4 h-4' />
                        Luogo:
                      </span>
                      <span className='font-semibold'>{experience?.place}</span>
                    </div>
                    <div className='gap-2 my-2 sm:flex'>
                      <span className='flex items-center gap-1.5 text-gray-600'>
                        <User className='w-4 h-4' />
                        Organizzato da:
                      </span>
                      <span className='font-semibold'>
                        {experience?.organiser}
                      </span>
                    </div>
                    <div className='gap-2 my-2 sm:flex'>
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
                    {experience.images.length > 0 && (
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
                      <h4 className='mb-3 text-xl font-semibold'>
                        Cosa è incluso
                      </h4>
                      <p className='text-gray-600'>
                        {experience?.includedDescription}
                      </p>
                    </div>

                    {experience?.carryWiths &&
                      experience?.carryWiths.length > 0 && (
                        <div className='my-3'>
                          <h4 className='mb-3 text-xl font-semibold'>
                            Cosa portare
                          </h4>
                          <ul className='ps-6'>
                            {experience.carryWiths.map((element) => (
                              <li
                                key={element.carryWithId}
                                className='list-disc'
                              >
                                {element.name}
                              </li>
                            ))}
                          </ul>
                        </div>
                      )}

                    <div className='my-3'>
                      <h4 className='mb-3 text-xl font-semibold'>
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
                <div className='sticky px-6 py-6 -translate-y-10 bg-white shadow-lg rounded-2xl top-32'>
                  <h4 className='mb-1 text-lg text-gray-500'>Prezzo totale</h4>
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
                      handleAddToCart(experience);
                    }}
                  >
                    Aggiungi al carrello
                  </Button>

                  <hr className='my-5 text-gray-500/30' />

                  <h4 className='mb-2 font-semibold'>
                    Informazioni importanti
                  </h4>

                  <div className='sm:flex items-center gap-1.5 mb-2'>
                    <UsersRound className='inline-block w-4 h-4 me-2' />
                    <span>
                      Esperienze per{' '}
                      <span className='font-semibold'>
                        {experience.maxParticipants}
                      </span>
                    </span>
                  </div>

                  {experience.maxParticipants > 1 && (
                    <div className='sm:flex items-center gap-1.5 mb-2'>
                      <span className='flex justify-start items-center gap-1.5'>
                        <HandCoins className='w-4 h-4' />
                        Costo per persona:
                      </span>
                      <span className='font-semibold'>
                        {(experience.price / experience.maxParticipants)
                          .toFixed(2)
                          .replace('.', ',')}
                        €
                      </span>
                    </div>
                  )}

                  <div className='sm:flex items-center gap-1.5 mb-2'>
                    <span className='flex justify-start items-center gap-1.5'>
                      <Clock className='w-4 h-4' />
                      Durata complessiva:
                    </span>
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
        )
      )}
    </div>
  );
};

export default DetailPage;

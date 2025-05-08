import React from 'react';
import Button from '../components/ui/Button';
import { Link, useNavigate } from 'react-router-dom';
import { ShoppingCart, Trash2 } from 'lucide-react';
import { useDispatch, useSelector } from 'react-redux';
import {
  addExperienceUnitToLocalCart,
  cartModified,
  removeExperienceFromLocalCart,
  removeExperienceUnitFromLocalCart,
} from '../redux/actions';
import { toast } from 'sonner';

const CartPage = () => {
  const backendUrl = import.meta.env.VITE_BACKEND_URL;

  const cart = useSelector((state) => {
    return state.cart;
  });

  const fidelityCard = useSelector((state) => {
    return state.fidelityCard;
  });

  const dispatch = useDispatch();

  const navigate = useNavigate();

  const shippingPrice = fidelityCard?.points >= 1000 ? 0 : 4.99;

  const cartTotal = () => {
    let totalPrice = cart.experiences.reduce(
      (sum, element) => sum + element.price * element.quantity,
      0
    );

    return totalPrice;
  };

  const cartTotalExperienceDiscount = () => {
    let totalDiscount = cart.experiences.reduce(
      (sum, element) =>
        sum + element.price * (element.sale / 100) * element.quantity,
      0
    );

    return totalDiscount;
  };

  const removeExperienceUnitManage = (experienceId) => {
    switch (cart.cartId !== '') {
      case true:
        removeExperienceUnit(experienceId);
        break;

      case false:
        removeExperienceUnitFromLocalCartFunction(experienceId);
        break;

      default:
        toast.error(
          <>
            <p className='font-bold'>Errore!</p>
            <p>Errore nella modifica del carrello</p>
          </>
        );
        break;
    }
  };

  const removeExperienceUnit = async (experienceId) => {
    try {
      const response = await fetch(
        `${backendUrl}/Cart/RemoveExperienceUnit/${cart.cartId}`,
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(experienceId),
        }
      );
      if (response.ok) {
        dispatch(cartModified());
      } else {
        throw new Error('Errore nella modifica del carrello!');
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

  const removeExperienceUnitFromLocalCartFunction = (experienceId) => {
    dispatch(removeExperienceUnitFromLocalCart(experienceId));
  };

  const addExperienceUnitManage = (experienceId) => {
    switch (cart.cartId !== '') {
      case true:
        addExperienceUnit(experienceId);
        break;

      case false:
        addExperienceUnitToLocalCartFunction(experienceId);
        break;

      default:
        toast.error(
          <>
            <p className='font-bold'>Errore!</p>
            <p>Errore nella modifica del carrello</p>
          </>
        );
        break;
    }
  };

  const addExperienceUnit = async (experienceId) => {
    try {
      const response = await fetch(
        `${backendUrl}/Cart/AddExperienceUnit/${cart.cartId}`,
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(experienceId),
        }
      );
      if (response.ok) {
        dispatch(cartModified());
      } else {
        throw new Error('Errore nella modifica del carrello!');
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

  const addExperienceUnitToLocalCartFunction = (experienceId) => {
    dispatch(addExperienceUnitToLocalCart(experienceId));
  };

  const removeExperienceFromCartManage = (experienceId) => {
    switch (cart.cartId !== '') {
      case true:
        removeExperienceFromCart(experienceId);
        break;

      case false:
        removeExperienceFromLocalCartFunction(experienceId);
        break;

      default:
        toast.error(
          <>
            <p className='font-bold'>Errore!</p>
            <p>Errore nella modifica del carrello</p>
          </>
        );
        break;
    }
  };

  const removeExperienceFromCart = async (experienceId) => {
    try {
      const response = await fetch(
        `${backendUrl}/Cart/RemoveExperience/${cart.cartId}`,
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(experienceId),
        }
      );
      if (response.ok) {
        dispatch(cartModified());

        toast.success(
          <>
            <p className='font-bold'>Carrello modificato!</p>
            <p>Esperienza rimossa dal carrello!</p>
          </>
        );
      } else {
        throw new Error('Errore nella modifica del carrello!');
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

  const removeExperienceFromLocalCartFunction = (experienceId) => {
    dispatch(removeExperienceFromLocalCart(experienceId));
    toast.success(
      <>
        <p className='font-bold'>Carrello modificato!</p>
        <p>Esperienza rimossa dal carrello!</p>
      </>
    );
  };

  return (
    <div className='min-h-screen px-6 mx-auto max-w-7xl xl:px-0'>
      <h1 className='mt-10 mb-8 text-4xl font-bold'>Carrello</h1>
      {cart.experiences != undefined && cart.experiences.length > 0 ? (
        <div className='grid grid-cols-3 gap-10 mb-10'>
          {/* colonna tabella esperienze */}
          <div className='col-span-3 px-4 py-6 overflow-x-auto bg-white shadow-lg lg:col-span-2 rounded-2xl'>
            <table className='w-full'>
              <thead>
                <tr className='grid gap-4 pb-3 border-b grid-cols-24 border-gray-400/30'>
                  <th className='hidden col-span-3 text-sm font-medium text-gray-500 md:block'>
                    Prodotto
                  </th>
                  <th className='col-span-12 text-sm font-medium text-gray-500 md:col-span-10'>
                    Dettagli
                  </th>
                  <th className='col-span-5 text-sm font-medium text-gray-500'>
                    Quantità
                  </th>
                  <th className='col-span-5 text-sm font-medium text-gray-500 md:col-span-4'>
                    Prezzo
                  </th>
                  <th className='col-span-2'></th>
                </tr>
              </thead>
              <tbody>
                {cart.experiences.map((exp) => (
                  <tr
                    key={exp.experienceId}
                    className='grid items-center gap-4 px-2 py-3 border-b grid-cols-24 hover:bg-gray-100 border-gray-400/30 last-of-type:border-0'
                  >
                    <td className='hidden col-span-3 overflow-hidden md:block'>
                      <div className='overflow-hidden rounded-lg aspect-square'>
                        <img
                          src={`https://klicko-backend-api.azurewebsites.net/uploads/${exp.coverImage}`}
                          className='object-cover w-full h-full'
                        />
                      </div>
                    </td>

                    <td className='col-span-11 md:col-span-10'>
                      <Link
                        to={`/experiences/detail/${exp.experienceId}`}
                        className='text-sm hover:text-primary'
                      >
                        <h6>{exp.title}</h6>
                      </Link>
                      <p className='text-gray-500 text-sm font-normal my-0.5'>
                        {exp.place}
                      </p>
                      <p className='text-sm font-normal text-gray-500'>
                        {exp.duration}
                      </p>
                    </td>

                    <td className='grid items-center justify-center grid-cols-4 col-span-5 gap-2'>
                      <Button
                        variant='outline'
                        size='sm'
                        className='order-2 w-8 h-8 col-span-2 text-xl font-bold md:order-1 md:col-span-1'
                        onClick={() => {
                          removeExperienceUnitManage(exp.experienceId);
                        }}
                      >
                        -
                      </Button>

                      <span className='flex items-center justify-center order-1 h-8 col-span-4 border md:order-2 md:col-span-2 bg-background border-gray-400/30 rounded-xl'>
                        {exp.quantity}
                      </span>

                      <Button
                        variant='outline'
                        size='sm'
                        className='order-3 w-8 h-8 col-span-2 text-xl font-bold md:order-3 md:col-span-1'
                        onClick={() => {
                          addExperienceUnitManage(exp.experienceId);
                        }}
                      >
                        +
                      </Button>
                    </td>

                    <td className='col-span-6 md:col-span-4'>
                      <p className='mb-1 text-sm font-semibold text-center md:text-lg'>
                        {(exp.price * (1 - exp.sale / 100) * exp.quantity)
                          .toFixed(2)
                          .replace('.', ',')}{' '}
                        €
                      </p>
                      <p className='text-xs font-normal text-center text-gray-500 sm:text-sm'>
                        {exp.quantity} x{' '}
                        {exp.price.toFixed(2).replace('.', ',')} €
                        {exp.sale > 0 && (
                          <span className='block mt-1 text-xs font-normal text-gray-500 sm:text-sm ms-2'>
                            -{exp.sale}%
                          </span>
                        )}
                      </p>
                    </td>

                    <td className='col-span-2 pe-1'>
                      <Trash2
                        className='w-4 h-4 text-red-500 cursor-pointer ms-auto'
                        onClick={() => {
                          removeExperienceFromCartManage(exp.experienceId);
                        }}
                      />
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>

          {/* colonna resoconto */}
          <div className='col-span-3 px-4 py-6 bg-white shadow-lg lg:col-span-1 rounded-2xl h-fit'>
            <h3 className='mb-4 text-xl font-semibold'>Riepilogo ordine</h3>
            <div className='items-center justify-between my-3 text-gray-600 xs:flex'>
              <span className='block mb-1 sm:mb-0'>
                Subtotale ({cart.experiences.length} esperienze)
              </span>
              <span className='block'>
                {cartTotal().toFixed(2).replace('.', ',')} €
              </span>
            </div>
            {cartTotalExperienceDiscount() > 0 && (
              <div className='items-center justify-between my-3 text-gray-600 sm:flex'>
                <span className='block mb-1 sm:mb-0'>Totale sconti</span>
                <span>
                  -{cartTotalExperienceDiscount().toFixed(2).replace('.', ',')}{' '}
                  €
                </span>
              </div>
            )}

            <div className='flex items-center justify-between my-3 text-gray-600'>
              <span>Costo di spedizione</span>
              <span>{shippingPrice.toFixed(2).replace('.', ',')} €</span>
            </div>

            <hr className='my-3 text-gray-400/30' />

            <div className='flex items-center justify-between my-5 font-semibold text-gray-600'>
              <span>Totale</span>
              <span>
                {(cartTotal() - cartTotalExperienceDiscount() + shippingPrice)
                  .toFixed(2)
                  .replace('.', ',')}{' '}
                €
              </span>
            </div>

            <Button
              variant='primary'
              fullWidth={true}
              className='mb-3'
              onClick={() => {
                navigate(`${cart.cartId === '' ? '/login' : '/checkout'}`);
              }}
            >
              Procedi al checkout
            </Button>

            <Link
              to='/experiences'
              className='text-sm text-primary hover:underline'
            >
              Continua lo shopping
            </Link>
          </div>
        </div>
      ) : (
        <div>
          <div className='flex flex-col min-h-[60vh] justify-center items-center bg-white rounded-2xl shadow-lg'>
            <ShoppingCart className='w-20 h-20 mb-3 text-gray-400/60' />
            <h3 className='mb-3 text-xl font-semibold'>
              Il tuo carrello è vuoto
            </h3>
            <p className='mb-5 text-center text-gray-500 px-26'>
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
      )}
    </div>
  );
};

export default CartPage;

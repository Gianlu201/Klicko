import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import Button from '../components/ui/Button';
import { useNavigate } from 'react-router-dom';
import { emptyCart } from '../redux/actions';
import { toast } from 'sonner';

const CheckOutPage = () => {
  const cart = useSelector((state) => {
    return state.cart;
  });

  const navigate = useNavigate();

  const dispatch = useDispatch();

  const getTotalPrice = () => {
    let totalPrice = cart.experiences.reduce(
      (sum, element) => sum + element.price * element.quantity,
      0
    );

    // console.log(totalPrice);

    return totalPrice.toFixed(2).replace('.', ',');
  };

  const sendOrder = async () => {
    try {
      let tokenObj = localStorage.getItem('klicko_token');

      if (!tokenObj) {
        navigate('/login');
      }

      let token = JSON.parse(tokenObj).token;

      const experiencesList = [];

      cart.experiences.forEach((exp) => {
        experiencesList.push({
          experienceId: exp.experienceId,
          quantity: exp.quantity,
        });
      });

      const body = {
        orderExperiences: experiencesList,
      };

      const response = await fetch(`https://localhost:7235/api/Order`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(body),
      });
      if (response.ok) {
        const data = await response.json();

        dispatch(emptyCart());
        navigate('/');
        toast.success('Acquisto effettuato con successo!');

        // dispatch(cartModified());
        // console.log(data.cart);

        console.log(data);
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch {
      console.log('Error');
    }
  };

  return (
    <div className='max-w-7xl mx-auto min-h-screen mt-8'>
      <h1 className='text-3xl font-bold mb-6'>Completa il tuo ordine</h1>
      <div className='grid grid-cols-3 justify-between items-start gap-6'>
        {/* riepilogo ordine */}
        <div className='col-span-1 bg-white rounded-lg px-6 py-5 shadow'>
          <h2 className='text-2xl font-bold mb-2'>Riepilogo ordine</h2>
          <p className='text-gray-500 mb-5'>
            {cart.experiences.length} esperienze
          </p>

          <div className='mb-5'>
            {cart.experiences.length > 0 &&
              cart.experiences.map((exp, index) => (
                <div
                  key={index}
                  className='grid grid-cols-12 items-center gap-2 border-b border-gray-400/40 py-3'
                >
                  <div className='col-span-1 font-semibold text-end'>
                    {exp.quantity}x
                  </div>
                  <div className='col-span-8 text-sm'>{exp.title}</div>
                  <div className='col-span-3 text-end font-semibold'>
                    {exp.price.toFixed(2).replace('.', ',')} €
                  </div>
                </div>
              ))}
          </div>

          <div className='flex justify-between items-center'>
            <span className='text-xl font-bold'>Totale</span>
            <span className='text-lg font-bold'>{getTotalPrice()} €</span>
          </div>
        </div>

        {/* modalità pagamento */}
        <div className='col-span-2 bg-white rounded-lg px-6 py-5 shadow'>
          <h2 className='text-2xl font-bold mb-2'>Pagamento</h2>
          <form className='pb-6'>
            <div className='flex flex-col gap-2 my-5'>
              <label htmlFor='name' className='text-sm font-medium'>
                Nome sulla carta
              </label>
              <input
                type='text'
                id='name'
                className='bg-background border border-gray-400/40 rounded-lg px-3 py-1.5'
                placeholder='Mario Rossi'
              />
            </div>

            <div className='flex flex-col gap-2 my-5'>
              <label htmlFor='cardNumber' className='text-sm font-medium'>
                Numero carta
              </label>
              <input
                type='number'
                id='cardNumber'
                className='bg-background border border-gray-400/40 rounded-lg px-3 py-1.5'
                placeholder='4242 4242 4242 4242'
              />
            </div>

            <div className='flex justify-between items-start gap-6 my-5'>
              <div className='w-full flex flex-col gap-2 my-5'>
                <label htmlFor='expireDate' className='text-sm font-medium'>
                  Scadenza
                </label>
                <input
                  type='text'
                  id='expireDate'
                  className='bg-background border border-gray-400/40 rounded-lg px-3 py-1.5'
                  placeholder='MM/YY'
                />
              </div>

              <div className='w-full flex flex-col gap-2 my-5'>
                <label htmlFor='secretCode' className='text-sm font-medium'>
                  CVV
                </label>
                <input
                  type='number'
                  id='secretCode'
                  className='bg-background border border-gray-400/40 rounded-lg px-3 py-1.5'
                  placeholder='123'
                />
              </div>
            </div>

            <Button variant='primary' fullWidth={true} onClick={sendOrder}>
              Paga {getTotalPrice()} €
            </Button>
          </form>
        </div>
      </div>
    </div>
  );
};

export default CheckOutPage;

import React from 'react';
import { loadStripe } from '@stripe/stripe-js';
import { Elements } from '@stripe/react-stripe-js';
import CheckoutForm from './CheckoutForm';

// const stripePromise = loadStripe('pk_test_');
const stripePublicKey = loadStripe(import.meta.env.VITE_STRIPE_PUBLIC_KEY);

const StripeContainer = ({ sendOrder, orderAmount }) => {
  console.log(stripePublicKey);
  return (
    <Elements stripe={stripePublicKey}>
      <CheckoutForm sendOrder={sendOrder} orderAmount={orderAmount} />
    </Elements>
  );
};

export default StripeContainer;

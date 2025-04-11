import { jwtDecode } from 'jwt-decode';
import { toast } from 'sonner';

export const SET_LOGGED_USER = 'SET_LOGGED_USER';
export const LOGOUT = 'LOGOUT';

export const setLoggedUser = (data) => {
  const tokenDecoded = jwtDecode(data.token);

  const userInfos = {
    aud: tokenDecoded.aud,
    exp: tokenDecoded.exp,
    role: tokenDecoded[
      'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
    ],
    email: tokenDecoded.email,
    name: tokenDecoded.name,
    surname: tokenDecoded.surname,
    id: tokenDecoded.nameidentifier,
    cartId: tokenDecoded.cartId,
    iss: tokenDecoded.iss,
    expiration: data.expires,
  };

  // console.log(userInfos);

  toast.success(`Bentornato ${userInfos.name}`);

  return {
    type: SET_LOGGED_USER,
    payload: userInfos,
  };
};

export const logoutUser = () => {
  localStorage.removeItem('klicko_token');
  return {
    type: LOGOUT,
  };
};

import React, { useState, useEffect } from "react";

const AuthContext = React.createContext({
  token: "",
  isLogin: false,
  onLogout: () => {},
  onLogin: (token) => {},
});

export const AuthContextProvider = (props) => {
  const storedUserLoginInformation = localStorage.getItem("token");
  const initialTokenData = !!storedUserLoginInformation;
  const [isLogin, setIsLogin] = useState(initialTokenData);
  const [token, setToken] = useState(initialTokenData);

  const logoutHandler = () => {
    localStorage.removeItem("token");
    setIsLogin(false);
  };

  const loginHandler = (token) => {
    localStorage.setItem("token", token );
    setIsLogin(true);
  };

  return (
    <AuthContext.Provider
      value={{
        token: token,
        isLogin: isLogin,
        onLogout: logoutHandler,
        onLogin: loginHandler,
      }}
    >
      {props.children}
    </AuthContext.Provider>
  );
};

export default AuthContext;

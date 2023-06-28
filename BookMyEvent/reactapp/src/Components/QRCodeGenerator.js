import React from 'react';
import QRCode from 'qrcode.react';

const QRCodeGenerator = ({ data }) => {
  return (
    <div>
      <QRCode value={data} />
    </div>
  );
};

export default QRCodeGenerator;

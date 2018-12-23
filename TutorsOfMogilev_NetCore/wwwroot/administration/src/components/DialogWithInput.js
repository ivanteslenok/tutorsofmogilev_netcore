import React, { createRef } from 'react';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogTitle from '@material-ui/core/DialogTitle';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogActions from '@material-ui/core/DialogActions';

export default function DialogWithInput({
  isOpen,
  handleClose,
  handleAccept,
  inputValue,
  title,
  description
}) {
  let input = createRef();

  const onAccept = () => {
    if (input.current.value.length < 1) return;

    handleAccept(input.current.value);
    handleClose();
  };

  return (
    <Dialog fullWidth maxWidth='sm' open={isOpen} onClose={handleClose}>
      <DialogTitle>{title}</DialogTitle>
      <DialogContent>
        <DialogContentText>{description}</DialogContentText>
        <TextField
          defaultValue={inputValue}
          inputRef={input}
          onKeyPress={ev => {
            ev.key === 'Enter' && onAccept();
          }}
          required
          autoFocus
          fullWidth
        />
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose} color="primary">
          Отмена
        </Button>
        <Button onClick={onAccept} color="primary">
          Готово
        </Button>
      </DialogActions>
    </Dialog>
  );
}



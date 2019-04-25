import React from 'react';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogTitle from '@material-ui/core/DialogTitle';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogActions from '@material-ui/core/DialogActions';

const DeleteConfirmDialog = ({ isOpen, handleClose, handleConfirm, text }) => (
  <Dialog open={isOpen} onClose={handleClose}>
    <DialogTitle>Подтвердите удаление</DialogTitle>
    <DialogContent>
      <DialogContentText>{text}</DialogContentText>
    </DialogContent>
    <DialogActions>
      <Button onClick={handleClose} color="primary">
        Отмена
      </Button>
      <Button onClick={handleConfirm} color="primary">
        Подтверждаю
      </Button>
    </DialogActions>
  </Dialog>
);

export default DeleteConfirmDialog;

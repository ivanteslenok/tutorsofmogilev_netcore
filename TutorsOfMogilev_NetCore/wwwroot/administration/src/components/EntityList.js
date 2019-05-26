import _ from 'lodash';
import React, { useState, useEffect } from 'react';
import EditableList from './EditableList';
import DeleteConfirmDialog from './DeleteConfirmDialog';
import DialogWithInput from './DialogWithInput';
import CreateBtn from './CreateBtn';
import Loading from './Loading';
import Paper from '@material-ui/core/Paper';

const EntityList = props => {
  const [changingItemId, setChangingItemId] = useState(null);
  const [isEdit, setIsEdit] = useState(false);
  const [deleteDialogOpen, togleDeleteDialog] = useState(false);
  const [inputDialogOpen, togleInputDialog] = useState(false);

  useEffect(() => {
    const { loading, loaded, loadingFailed, loadData } = props;
    if (!loaded && !loading && !loadingFailed) loadData();
  });

  const openDeleteDialog = () => togleDeleteDialog(true);

  const closeDeleteDialog = () => togleDeleteDialog(false);

  const openInputDialog = () => togleInputDialog(true);
  
  const closeInputDialog = () => togleInputDialog(false);

  const handleEditStart = id => {
    setChangingItemId(id);
    setIsEdit(true);
    openInputDialog();
  };

  const handleDeleteStart = id => {
    setChangingItemId(id);
    setIsEdit(false);
    openDeleteDialog();
  };

  const handleDeleteConfirm = () => {
    props.remove(changingItemId);
    closeDeleteDialog();
  };

  const handleAccept = value => {
    const { update, create } = props;

    isEdit ? update(changingItemId, { name: value }) : create({ name: value });

    setChangingItemId(null);
    setIsEdit(false);
  };

  const handleEditClose = () => {
    setChangingItemId(null);
    setIsEdit(false);
    closeInputDialog();
  };

  const {
    items,
    loading,
    loadingFailed
  } = props;

  const description = 'Введите название.';

  const deleteItem = changingItemId !== null && _.find(items, { id: changingItemId });
  const deleteText = deleteItem && deleteItem.name;

  let paperStyle = {
    width: '50%',
    minHeight: '100px',
    margin: '0 auto',
    position: 'relative'
  };

  let content = (
    <>
      <EditableList
        items={items}
        handleItemEdit={handleEditStart}
        handleItemDelete={handleDeleteStart}
      />
      <div>
        <CreateBtn handleClick={openInputDialog} />
      </div>
      <DeleteConfirmDialog
        isOpen={deleteDialogOpen}
        text={deleteText}
        handleClose={closeDeleteDialog}
        handleConfirm={handleDeleteConfirm}
      />
      <DialogWithInput
        title={isEdit ? 'Редактирование' : 'Создание'}
        description={description}
        isOpen={inputDialogOpen}
        handleClose={isEdit ? handleEditClose : closeInputDialog}
        inputValue={isEdit ? _.find(items, { id: changingItemId }).name : ''}
        handleAccept={handleAccept}
      />
    </>
  );

  if (loading) content = <Loading />;

  if (loadingFailed) {
    content = <h2 style={{ color: '#f00' }}>Ошибка загрузки</h2>;
    paperStyle = {
      ...paperStyle,
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center'
    };
  }

  return <Paper style={paperStyle}>{content}</Paper>;
};

export default EntityList;

import _ from 'lodash';
import React, { useState, useEffect } from 'react';
import EditableList from './EditableList';
import DeleteConfirmDialog from './DeleteConfirmDialog';
import DialogWithInput from './DialogWithInput';
import CreateBtn from './CreateBtn';
import Loading from './Loading';
import Paper from '@material-ui/core/Paper';

const EnityList = props => {
  const [changingItemId, setChangingItemId] = useState(null);
  const [isEdit, setIsEdit] = useState(false);

  useEffect(() => {
    const { loading, loaded, loadData } = props;
    if (!loaded && !loading) loadData();
  });

  const handleEditStart = id => {
    setChangingItemId(id);
    setIsEdit(true);
    props.openInputDialog();
  };

  const handleDeleteStart = id => {
    setChangingItemId(id);
    setIsEdit(false);
    props.openDeleteDialog();
  };

  const handleDeleteConfirm = () => {
    props.remove(changingItemId);
    props.closeDeleteDialog();
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
    props.closeInputDialog();
  };

  const {
    items,
    loading,
    deleteDialogOpen,
    closeDeleteDialog,
    withInputDialogOpen,
    openInputDialog,
    closeInputDialog
  } = props;

  const description = 'Введите название.';

  const deleteItem =
    changingItemId !== null && _.find(items, { id: changingItemId });
  const deleteText = deleteItem && deleteItem.name;

  const content = loading ? (
    <Loading />
  ) : (
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
        isOpen={withInputDialogOpen}
        handleClose={isEdit ? handleEditClose : closeInputDialog}
        inputValue={isEdit ? _.find(items, { id: changingItemId }).name : ''}
        handleAccept={handleAccept}
      />
    </>
  );

  return (
    <Paper
      style={{
        width: '50%',
        minHeight: '100px',
        margin: '0 auto',
        position: 'relative'
      }}
    >
      {content}
    </Paper>
  );
};

export default EnityList;

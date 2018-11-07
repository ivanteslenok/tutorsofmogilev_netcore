import _ from 'lodash';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import { getSortedSpecializations } from './../selectors/specializations';
import * as specializationAC from '../AC/specialization';
import * as dialogsAC from '../AC/dialogs';
import EditableList from '../components/EditableList/index';
import DeleteConfirmDialog from '../components/DeleteConfirmDialog';
import DialogWithInput from '../components/DialogWithInput';
import CreateBtn from '../components/CreateBtn';
import Loading from '../components/Loading';
import Paper from '@material-ui/core/Paper';

class SpecializationsList extends Component {
  state = {
    changingItemId: null,
    isEdit: false
  };

  componentDidMount() {
    const { loading, loaded, loadData } = this.props;
    if (!loaded && !loading) loadData();
  }

  handleEditStart = id => {
    this.setState({ changingItemId: id, isEdit: true });
    this.props.openInputDialog();
  };

  handleDeleteStart = id => {
    this.setState({ changingItemId: id, isEdit: false });
    this.props.openDeleteDialog();
  };

  handleAccept = value => {
    const { update, create } = this.props;
    const { changingItemId, isEdit } = this.state;

    isEdit ? update(changingItemId, { name: value }) : create({ name: value });

    this.setState({ changingItemId: null, isEdit: false });
  };

  handleEditClose = () => {
    this.setState({ changingItemId: null, isEdit: false });
    this.props.closeInputDialog();
  };

  render() {
    const {
      items,
      loading,

      deleteDialogOpen,
      closeDeleteDialog,

      withInputDialogOpen,
      openInputDialog,
      closeInputDialog,

      remove
    } = this.props;

    const { changingItemId, isEdit } = this.state;

    const description = 'Введите название. Не более 100 символов.';

    const content = loading ? (
      <Loading />
    ) : (
      <>
        <EditableList
          items={items}
          handleItemEdit={this.handleEditStart}
          handleItemDelete={this.handleDeleteStart}
        />
        <div>
          <CreateBtn handleClick={openInputDialog} />
        </div>
        <DeleteConfirmDialog
          isOpen={deleteDialogOpen}
          handleClose={closeDeleteDialog}
          handleConfirm={() => remove(changingItemId)}
        />
        <DialogWithInput
          title={isEdit ? 'Редактирование' : 'Создание'}
          description={description}
          isOpen={withInputDialogOpen}
          handleClose={isEdit ? this.handleEditClose : closeInputDialog}
          inputValue={isEdit ? _.find(items, { id: changingItemId }).name : ''}
          handleAccept={this.handleAccept}
        />
      </>
    );

    return <Paper style={{ width: '50%', margin: '0 auto' }}>{content}</Paper>;
  }
}

const mapStateToProps = state => {
  return {
    items: getSortedSpecializations(state),
    loading: state.specializations.loading,
    loaded: state.specializations.loaded,
    deleteDialogOpen: state.dialogs.deleteConfirmOpen,
    withInputDialogOpen: state.dialogs.withInputOpen
  };
};

const mapDispatchToProps = dispatch => {
  return {
    loadData: () => {
      dispatch(specializationAC.loadSpecializations());
    },
    remove: id => {
      dispatch(specializationAC.removeSpecialization(id));
    },
    create: specialization => {
      dispatch(specializationAC.createSpecialization(specialization));
    },
    update: (id, specialization) => {
      dispatch(specializationAC.updateSpecialization(id, specialization));
    },
    openDeleteDialog: () => {
      dispatch(dialogsAC.openDeleteConfirmDialog());
    },
    closeDeleteDialog: () => {
      dispatch(dialogsAC.closeDeleteConfirmDialog());
    },
    openInputDialog: () => {
      dispatch(dialogsAC.openDialogWithInput());
    },
    closeInputDialog: () => {
      dispatch(dialogsAC.closeDialogWithInput());
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(SpecializationsList);

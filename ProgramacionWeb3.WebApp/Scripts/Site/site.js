var waitingDialog = waitingDialog || (function ($) {
    'use strict';

    // Creating modal dialog's DOM
    var $dialog = $(
        '<div class="modal fade" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-hidden="true" style="padding-top:15%; overflow-y:visible;">' +
        '<div class="modal-dialog modal-m">' +
        '<div class="modal-content">' +
        '<div class="modal-header"><h3 class="modal-title"></h3></div>' +
        '<div class="modal-body">' +
        '<div class="text-center">' +
        '<i class="fa fa-spinner fa-spin fa-5x fa-fw "></i>'+
        '</div></div></div></div></div></div>');

    return {
        /**
         * Abre el dialogo
         * @param message Mensaje
         * @param options Opciones:
         * 				  options.dialogSize - Tamaño por defecto sm;         
         */
        show: function (message, options) {
            // Assigning defaults
            if (typeof options === 'undefined') {
                options = {};
            }
            if (typeof message === 'undefined') {
                message = 'Cargando';
            }
            var settings = $.extend({
                dialogSize: 'sm',
                onHide: null 
            }, options);
            
            $dialog.find('.modal-dialog').attr('class', 'modal-dialog').addClass('modal-' + settings.dialogSize);
           
            $dialog.find('h3').text(message);
            
            if (typeof settings.onHide === 'function') {
                $dialog.off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
                    settings.onHide.call($dialog);
                });
            }
            
            $dialog.modal();
        },
        
        hide: function () {
            $dialog.modal('hide');
        }
    };

})(jQuery);
$(document).ready(function () {
    $('.datepicker').datepicker({
        format: 'dd/mm/yyyy',
        language: 'es'
    });
});
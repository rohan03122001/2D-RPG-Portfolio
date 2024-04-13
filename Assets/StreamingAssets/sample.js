const links = document.querySelectorAll('.external-link');

links.forEach(link => {
  link.addEventListener('click', event => {
    event.preventDefault();
    window.open(link.href, '_blank');
  });
});
# Use the official NGINX image as the base image
FROM nginx:latest

# Copy the custom NGINX configuration file to the container
COPY nginx.conf /etc/nginx/nginx.conf

# Copy the HTML content to the default NGINX root directory
#COPY index.html /usr/share/nginx/html/index.html

# Expose the default NGINX port
EXPOSE 80

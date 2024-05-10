# 📠 RediParser

This is a simple library to parse the REDIS protocol. It aims to provide a simple, yet performant parser that gets the job done and leaves the network stack implementation up to the consumer. You can find documentation about how to utilize this library [here](USAGE.md).

# Why leave out networking?

Programs can have a wide variety of configurations and implementations for how their networking is handled. To best work with your needs, RediParser doesn't seek to impose its own ideas of network structure on your project.

# Forewarning

This library is incomplete and may not totally adhere to RESP standards! As time goes on, support will be built out, but please consider this to be in an **ALPHA** stage.